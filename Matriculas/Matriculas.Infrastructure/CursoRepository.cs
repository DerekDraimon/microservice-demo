using Matriculas.Application;
using Matriculas.Domain;
using Microsoft.EntityFrameworkCore;

namespace Matriculas.Infrastructure;

public class CursoRepository : ICursoRepository
{
    private readonly MatriculasDbContext _db;

    public CursoRepository(MatriculasDbContext db) => _db = db;

    public Task<Curso?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _db.Cursos.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task AddAsync(Curso curso, CancellationToken cancellationToken = default)
    {
        _db.Cursos.Add(curso);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Curso curso, CancellationToken cancellationToken = default)
    {
        _db.Cursos.Update(curso);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
