using CatalogoCursos.Application;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoCursos.API.Controllers;

[ApiController]
[Route("cursos")]
public class CursosController : ControllerBase
{
    private readonly CrearCursoHandler _handler;

    public CursosController(CrearCursoHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CrearCursoCommand command, CancellationToken cancellationToken)
    {
        var id = await _handler.Handle(command, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id) => Ok(new { Id = id });
}
