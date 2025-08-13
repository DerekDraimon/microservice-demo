namespace CatalogoCursos.Domain;

public class Curso
{
    public Guid Id { get; init; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}
