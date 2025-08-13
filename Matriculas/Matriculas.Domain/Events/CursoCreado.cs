namespace Matriculas.Domain.Events;

public record CursoCreado(Guid CursoId, string Nombre, int Cupo);
