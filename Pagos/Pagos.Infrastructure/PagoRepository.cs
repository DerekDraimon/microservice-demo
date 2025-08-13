using Microsoft.EntityFrameworkCore;
using Pagos.Application;
using Pagos.Domain;

namespace Pagos.Infrastructure;

public class PagoRepository : IPagoRepository
{
    private readonly PagosDbContext _db;

    public PagoRepository(PagosDbContext db) => _db = db;

    public async Task AddAsync(Pago pago, CancellationToken cancellationToken = default)
    {
        _db.Pagos.Add(pago);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public Task<Pago?> GetByMatriculaIdAsync(Guid matriculaId, CancellationToken cancellationToken = default)
        => _db.Pagos.FirstOrDefaultAsync(p => p.MatriculaId == matriculaId, cancellationToken);
}
