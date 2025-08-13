namespace CatalogoCursos.Application;

public interface IEventBus
{
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default);
}
