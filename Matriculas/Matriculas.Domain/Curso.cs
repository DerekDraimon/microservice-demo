namespace Matriculas.Domain;

public class Curso
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Cupo { get; set; }
    public int Matriculados { get; set; }
}
