using CatalogoCursos.Application;
using CatalogoCursos.Domain;

namespace CatalogoCursos.Infrastructure;

public class CursoRepository : ICursoRepository
{
    private readonly CatalogoCursosDbContext _context;

    public CursoRepository(CatalogoCursosDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Curso curso, CancellationToken cancellationToken = default)
    {
        _context.Cursos.Add(curso);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
