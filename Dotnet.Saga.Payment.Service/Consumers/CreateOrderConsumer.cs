using System.Text;
using System.Text.Json;
using Dotnet.Saga.Payment.Service.Entities;
using Dotnet.Saga.Payment.Service.Enums;
using Dotnet.Saga.Payment.Service.Interfaces;
using Dotnet.Saga.Payment.Service.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Dotnet.Saga.Payment.Service.Consumers;

public class CreateOrderConsumer : BackgroundService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentService _paymentService;
    private readonly IBusService _busService;
    private readonly IConfiguration _configuration;

    private readonly ILogger<CreateOrderConsumer> _logger;

    public CreateOrderConsumer(IPaymentRepository paymentRepository, IPaymentService paymentService, IBusService busService, IConfiguration configuration, ILogger<CreateOrderConsumer> logger)
    {
        _paymentRepository = paymentRepository;
        _paymentService = paymentService;
        _busService = busService;
        _configuration = configuration;
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

            var paymentResult = await _paymentService.Send(data!);
            var payment = new PaymentEntity
            {
                OrderId = data!.Id,
                PaymentIdent = paymentResult.Ident,
                Value = data.TotalValue
            };

            payment.SetStatus(paymentResult.Status);

            await _paymentRepository.CreateAsync(payment);

            if (payment.Status == PaymentStatusEnum.Success)
            {
                await _busService.PublishPaymentSuccessAsync(new
                {
                    OrderId = payment.OrderId
                });
            }
            else if (payment.Status == PaymentStatusEnum.Error)
            {
                await _busService.PublishPaymentErrorAsync(new
                {
                    OrderId = payment.OrderId
                });
            }

            _logger.LogInformation("[Consumer][CreateOrder] Consume Message end!");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[Consumer][CreateOrder][Error] {ex.Message}");
        }
    }
}