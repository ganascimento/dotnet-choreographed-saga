using System.Text;
using System.Text.Json;
using Dotnet.Saga.Email.Service.Interfaces;
using Dotnet.Saga.Email.Service.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Dotnet.Saga.Email.Service.Consumers;

public class PaymentErrorConsumer : BackgroundService
{
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<PaymentErrorConsumer> _logger;

    public PaymentErrorConsumer(IEmailService emailService, IConfiguration configuration, ILogger<PaymentErrorConsumer> logger)
    {
        _emailService = emailService;
        _configuration = configuration;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connectionFactory = new ConnectionFactory { Uri = new Uri(_configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("Invalid RabbitMQ connection string!")) };
        var exchange = _configuration["Exchange:PaymentError"] ?? throw new InvalidOperationException("Exchange not found!");

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
            _logger.LogInformation("[Consumer][PaymentError] Consume Message start!");

            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var data = JsonSerializer.Deserialize<PaymentErrorConsumerModel>(message);

            await _emailService.SendPaymentErrorAsync(data!.OrderId);

            _logger.LogInformation("[Consumer][PaymentError] Consume Message end!");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[Consumer][PaymentError][Error] {ex.Message}");
        }
    }
}