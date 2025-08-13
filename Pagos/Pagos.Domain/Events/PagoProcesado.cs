namespace Pagos.Domain.Events;

public record PagoProcesado(Guid MatriculaId, string Estado);
