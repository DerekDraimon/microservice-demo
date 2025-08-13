using Pagos.Domain;

namespace Pagos.Application;

public interface IPagoRepository
{
    Task AddAsync(Pago pago, CancellationToken cancellationToken = default);
    Task<Pago?> GetByMatriculaIdAsync(Guid matriculaId, CancellationToken cancellationToken = default);
}
