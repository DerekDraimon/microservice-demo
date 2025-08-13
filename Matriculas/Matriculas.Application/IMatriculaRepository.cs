using Matriculas.Domain;

namespace Matriculas.Application;

public interface IMatriculaRepository
{
    Task AddAsync(Matricula matricula, CancellationToken cancellationToken = default);
}
