using Matriculas.Domain;

namespace Matriculas.Application;

public interface ICursoRepository
{
    Task<Curso?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Curso curso, CancellationToken cancellationToken = default);
    Task UpdateAsync(Curso curso, CancellationToken cancellationToken = default);
}
