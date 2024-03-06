using System.Text;
using System.Text.Json;
using Dotnet.Saga.Payment.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Dotnet.Saga.Payment.Service.Bus;

public class BusService : IBusService
{
    private readonly IConfiguration _configuration;
    private readonly ConnectionFactory _connectionFactory;

    public BusService(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionFactory = new ConnectionFactory { Uri = new Uri(configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("Invalid RabbitMQ connection string!")) };
    }

    public async Task PublishPaymentErrorAsync(object body)
    {
        var exchange = _configuration["Exchange:PaymentError"] ?? throw new InvalidOperationException("Exchange not found!");
        using var connection = _connectionFactory.CreateConnection();
        using var channel = _connectionFactory.CreateConnection().CreateModel();

        channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

        await Task.Run(() =>
        {
            channel.BasicPublish(exchange, "", null, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(body)));
        });
    }

    public async Task PublishPaymentSuccessAsync(object body)
    {
        var exchange = _configuration["Exchange:PaymentSuccess"] ?? throw new InvalidOperationException("Exchange not found!");
        using var connection = _connectionFactory.CreateConnection();
        using var channel = _connectionFactory.CreateConnection().CreateModel();

        channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

        await Task.Run(() =>
        {
            channel.BasicPublish(exchange, "", null, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(body)));
        });
    }
}