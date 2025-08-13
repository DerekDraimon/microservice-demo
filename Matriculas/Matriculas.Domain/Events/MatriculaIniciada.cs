namespace Matriculas.Domain.Events;

public record MatriculaIniciada(Guid MatriculaId, Guid CursoId, Guid EstudianteId);
