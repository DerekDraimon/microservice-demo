using Matriculas.Domain;
using Matriculas.Domain.Events;

namespace Matriculas.Application;

public class MatricularEstudianteHandler
{
    private readonly ICursoRepository _cursoRepository;
    private readonly IMatriculaRepository _matriculaRepository;
    private readonly IEventBus _eventBus;

    public MatricularEstudianteHandler(ICursoRepository cursoRepository, IMatriculaRepository matriculaRepository, IEventBus eventBus)
    {
        _cursoRepository = cursoRepository;
        _matriculaRepository = matriculaRepository;
        _eventBus = eventBus;
    }

    public async Task<Guid> Handle(MatricularEstudianteCommand command, CancellationToken cancellationToken = default)
    {
        var curso = await _cursoRepository.GetByIdAsync(command.CursoId, cancellationToken);
        if (curso is null || curso.Matriculados >= curso.Cupo)
            throw new InvalidOperationException("Curso no disponible");

        curso.Matriculados++;
        await _cursoRepository.UpdateAsync(curso, cancellationToken);

        var matricula = new Matricula
        {
            Id = Guid.NewGuid(),
            CursoId = command.CursoId,
            EstudianteId = command.EstudianteId
        };

        await _matriculaRepository.AddAsync(matricula, cancellationToken);

        var evento = new MatriculaIniciada(matricula.Id, matricula.CursoId, matricula.EstudianteId);
        await _eventBus.PublishAsync(evento, cancellationToken);

        return matricula.Id;
    }
}
