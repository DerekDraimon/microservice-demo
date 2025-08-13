using System.Text.Json;
using Matriculas.Application;
using RabbitMQ.Client;

namespace Matriculas.Infrastructure;

public class RabbitMqEventBus : IEventBus, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqEventBus(string hostName)
    {
        var factory = new ConnectionFactory { HostName = hostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default)
    {
        var exchange = "matriculas";
        _channel.ExchangeDeclare(exchange, ExchangeType.Fanout, durable: true);
        var body = JsonSerializer.SerializeToUtf8Bytes(@event);
        _channel.BasicPublish(exchange, string.Empty, null, body);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}
