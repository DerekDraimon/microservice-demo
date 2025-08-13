using System.Text;
using System.Text.Json;
using Matriculas.Application;
using Matriculas.Domain;
using Matriculas.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Matriculas.Infrastructure;

public class CursoCreadoListener : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public CursoCreadoListener(IServiceProvider provider)
    {
        _provider = provider;
        var factory = new ConnectionFactory { HostName = "rabbitmq" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _channel.ExchangeDeclare("catalogo-cursos", ExchangeType.Fanout, durable: true);
        var queue = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue, "catalogo-cursos", string.Empty);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (_, ea) =>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            var evento = JsonSerializer.Deserialize<CursoCreado>(json);
            if (evento is null) return;

            using var scope = _provider.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<ICursoRepository>();
            await repo.AddAsync(new Curso { Id = evento.CursoId, Nombre = evento.Nombre, Cupo = evento.Cupo }, stoppingToken);
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
