namespace Matriculas.Domain;

public class Matricula
{
    public Guid Id { get; set; }
    public Guid CursoId { get; set; }
    public Guid EstudianteId { get; set; }
    public string Estado { get; set; } = "Iniciada";
}
