using Microsoft.AspNetCore.Mvc;
using Pagos.Application;

namespace Pagos.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PagosController : ControllerBase
{
    private readonly IPagoRepository _repository;

    public PagosController(IPagoRepository repository) => _repository = repository;

    [HttpGet("{matriculaId}")]
    public async Task<IActionResult> Get(Guid matriculaId, CancellationToken cancellationToken)
    {
        var pago = await _repository.GetByMatriculaIdAsync(matriculaId, cancellationToken);
        if (pago is null) return NotFound();
        return Ok(pago);
    }
}
