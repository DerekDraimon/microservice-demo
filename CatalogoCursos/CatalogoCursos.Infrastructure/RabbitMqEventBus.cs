using System.Text;
using System.Text.Json;
using CatalogoCursos.Application;
using RabbitMQ.Client;

namespace CatalogoCursos.Infrastructure;

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
        var exchange = "catalogo-cursos";
        _channel.ExchangeDeclare(exchange, ExchangeType.Fanout, durable: true);
        var body = JsonSerializer.SerializeToUtf8Bytes(@event);
        _channel.BasicPublish(exchange, routingKey: string.Empty, basicProperties: null, body: body);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}
