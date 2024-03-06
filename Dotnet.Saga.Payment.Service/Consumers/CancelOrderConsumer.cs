using System.Text;
using System.Text.Json;
using Dotnet.Saga.Payment.Service.Enums;
using Dotnet.Saga.Payment.Service.Interfaces;
using Dotnet.Saga.Payment.Service.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Dotnet.Saga.Payment.Service.Consumers;

public class CancelOrderConsumer : BackgroundService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IBusService _busService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<CancelOrderConsumer> _logger;

    public CancelOrderConsumer(IPaymentRepository paymentRepository, IBusService busService, IConfiguration configuration, ILogger<CancelOrderConsumer> logger)
    {
        _paymentRepository = paymentRepository;
        _busService = busService;
        _configuration = configuration;
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

            var payment = await _paymentRepository.GetByIdAsync(data!.Id);
            if (payment == null)
                throw new KeyNotFoundException("Payment not found!");

            payment.SetStatus(PaymentStatusEnum.Canceled);

            await _paymentRepository.UpdateAsync(payment);

            _logger.LogInformation("[Consumer][CancelOrder] Consume Message end!");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[Consumer][CancelOrder][Error] {ex.Message}");
        }
    }
}