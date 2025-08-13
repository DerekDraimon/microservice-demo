using CatalogoCursos.Domain;
using CatalogoCursos.Domain.Events;

namespace CatalogoCursos.Application;

public class CrearCursoHandler
{
    private readonly ICursoRepository _repository;
    private readonly IEventBus _eventBus;

    public CrearCursoHandler(ICursoRepository repository, IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    public async Task<Guid> Handle(CrearCursoCommand command, CancellationToken cancellationToken = default)
    {
        var curso = new Curso
        {
            Id = Guid.NewGuid(),
            Nombre = command.Nombre,
            Descripcion = command.Descripcion
        };

        await _repository.AddAsync(curso, cancellationToken);

        var evento = new CursoCreado(curso.Id, curso.Nombre);
        await _eventBus.PublishAsync(evento, cancellationToken);

        return curso.Id;
    }
}
