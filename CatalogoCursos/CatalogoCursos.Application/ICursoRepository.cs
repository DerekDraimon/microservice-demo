using CatalogoCursos.Domain;

namespace CatalogoCursos.Application;

public interface ICursoRepository
{
    Task AddAsync(Curso curso, CancellationToken cancellationToken = default);
}
