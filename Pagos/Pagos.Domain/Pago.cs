namespace Pagos.Domain;

public class Pago
{
    public Guid Id { get; set; }
    public Guid MatriculaId { get; set; }
    public string Estado { get; set; } = string.Empty;
}
