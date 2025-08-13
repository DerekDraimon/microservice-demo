using Matriculas.Application;
using Matriculas.Domain;

namespace Matriculas.Infrastructure;

public class MatriculaRepository : IMatriculaRepository
{
    private readonly MatriculasDbContext _db;

    public MatriculaRepository(MatriculasDbContext db) => _db = db;

    public async Task AddAsync(Matricula matricula, CancellationToken cancellationToken = default)
    {
        _db.Matriculas.Add(matricula);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
