using System.Text;
using System.Text.Json;
using Dotnet.Saga.Stock.API.Interfaces;
using Dotnet.Saga.Stock.API.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Dotnet.Saga.Stock.API.Consumers;

public class CancelOrderConsumer : BackgroundService
{
    private readonly IStockRepository _stockRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<CancelOrderConsumer> _logger;

    public CancelOrderConsumer(IServiceProvider services, IConfiguration configuration, ILogger<CancelOrderConsumer> logger)
    {
        var scope = services.CreateScope();

        _configuration = configuration;
        _stockRepository = scope.ServiceProvider.GetRequiredService<IStockRepository>();
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connectionFactory = new ConnectionFactory { Uri = new Uri(_configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("Invalid RabbitMQ connection string!")) };
        var exchange = _configuration["Exchange:OrderCancel"] ?? throw new InvalidOperationException("Exchange not found!");

        using var connection = connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);
        var queueName = channel.QueueDeclare().QueueName;
        var consumer = new EventingBasicConsumer(channel);

        channel.QueueBind(queueName, exchange, "");
        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        consumer.Received += this.Consume;

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    private async void Consume(object? sender, BasicDeliverEventArgs e)
    {
        try
        {
            _logger.LogInformation("[Consumer][CancelOrder] Consume Message start!");

            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var data = JsonSerializer.Deserialize<CancelOrderConsumerModel>(message);

            var stocks = await _stockRepository.GetByOrderIdAsync(data!.Id);

            stocks?.ForEach(stock =>
            {
                var stockHistory = stock.StockHistory?.Where(x => x.Stock?.Product?.Id == stock.Product?.Id);
                _logger.LogInformation($"[Consumer][CancelOrder] Amount old: {stock.Amount}");

                stock.AddAmount(stockHistory?.Sum(x => x.Amount) ?? 0);
                stock.CancelHistories();

                _logger.LogInformation($"[Consumer][CancelOrder] Amount new: {stock.Amount}");
            });

            if (stocks != null)
                await _stockRepository.UpdateAsync(stocks, false);

            _logger.LogInformation("[Consumer][CancelOrder] Consume Message end!");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[Consumer][CancelOrder][Error] {ex.Message}");
        }
    }
}