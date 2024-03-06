using System.Text;
using System.Text.Json;
using Dotnet.Saga.Order.API.Interfaces;
using RabbitMQ.Client;

namespace Dotnet.Saga.Order.API.Bus;

public class BusService : IBusService
{
    private readonly IConfiguration _configuration;
    private readonly ConnectionFactory _connectionFactory;

    public BusService(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionFactory = new ConnectionFactory { Uri = new Uri(configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("Invalid RabbitMQ connection string!")) };
    }

    public async Task PublishCreateOrderAsync(object body)
    {
        var exchange = _configuration["Exchange:OrderCreate"] ?? throw new InvalidOperationException("Exchange not found!");
        using var connection = _connectionFactory.CreateConnection();
        using var channel = _connectionFactory.CreateConnection().CreateModel();

        channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

        await Task.Run(() =>
        {
            channel.BasicPublish(exchange, "", null, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(body)));
        });
    }

    public async Task PublishCancelOrderAsync(object body)
    {
        var exchange = _configuration["Exchange:OrderCancel"] ?? throw new InvalidOperationException("Exchange not found!");
        using var connection = _connectionFactory.CreateConnection();
        using var channel = _connectionFactory.CreateConnection().CreateModel();

        channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

        await Task.Run(() =>
        {
            channel.BasicPublish(exchange, "", null, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(body)));
        });
    }
}