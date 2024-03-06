using System.Text;
using System.Text.Json;
using Dotnet.Saga.Stock.API.Entities;
using Dotnet.Saga.Stock.API.Enums;
using Dotnet.Saga.Stock.API.Interfaces;
using Dotnet.Saga.Stock.API.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Dotnet.Saga.Stock.API.Consumers;

public class CreateOrderConsumer : BackgroundService
{
    private readonly IStockRepository _stockRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<PaymentErrorConsumer> _logger;

    public CreateOrderConsumer(IServiceProvider services, IConfiguration configuration, ILogger<PaymentErrorConsumer> logger)
    {
        var scope = services.CreateScope();

        _configuration = configuration;
        _stockRepository = scope.ServiceProvider.GetRequiredService<IStockRepository>();
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connectionFactory = new ConnectionFactory { Uri = new Uri(_configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("Invalid RabbitMQ connection string!")) };
        var exchange = _configuration["Exchange:OrderCreate"] ?? throw new InvalidOperationException("Exchange not found!");

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
            _logger.LogInformation("[Consumer][CreateOrder] Consume Message start!");

            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var data = JsonSerializer.Deserialize<CreateOrderConsumerModel>(message);

            var stocks = new List<StockEntity>();

            foreach (var product in data!.Products)
            {
                var stock = await _stockRepository.GetByProductIdAsync(product.ProductId);

                if (stock == null)
                    throw new KeyNotFoundException("Stock not found!");

                stock.AddHistory(product.Amount, data.Id, StockStatusEnum.Created);
                stock.GetAmount(product.Amount);

                stocks.Add(stock);
            }

            await _stockRepository.UpdateAsync(stocks);

            _logger.LogInformation("[Consumer][CreateOrder] Consume Message end!");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[Consumer][CreateOrder][Error] {ex.Message}");
        }
    }
}