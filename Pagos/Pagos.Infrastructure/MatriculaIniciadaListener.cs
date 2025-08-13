using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pagos.Domain.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Pagos.Infrastructure;

public class MatriculaIniciadaListener : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MatriculaIniciadaListener(IServiceProvider provider)
    {
        _provider = provider;
        var factory = new ConnectionFactory { HostName = "rabbitmq" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _channel.ExchangeDeclare("matriculas", ExchangeType.Fanout, durable: true);
        var queue = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue, "matriculas", string.Empty);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (_, ea) =>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            var evento = JsonSerializer.Deserialize<MatriculaIniciada>(json);
            if (evento is null) return;

            using var scope = _provider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<Pagos.Application.ProcesarPagoHandler>();
            await handler.Handle(evento, stoppingToken);
        };

        _channel.BasicConsume(queue, true, consumer);
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
        base.Dispose();
    }
}
