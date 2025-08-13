using Pagos.Domain;
using Pagos.Domain.Events;

namespace Pagos.Application;

public class ProcesarPagoHandler
{
    private readonly IPagoRepository _repository;
    private readonly IEventBus _eventBus;
    private readonly Random _random = new();

    public ProcesarPagoHandler(IPagoRepository repository, IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    public async Task Handle(MatriculaIniciada evento, CancellationToken cancellationToken = default)
    {
        var pago = new Pago
        {
            Id = Guid.NewGuid(),
            MatriculaId = evento.MatriculaId,
            Estado = _random.Next(2) == 0 ? "Aprobado" : "Rechazado"
        };

        await _repository.AddAsync(pago, cancellationToken);

        var procesado = new PagoProcesado(pago.MatriculaId, pago.Estado);
        await _eventBus.PublishAsync(procesado, cancellationToken);
    }
}
