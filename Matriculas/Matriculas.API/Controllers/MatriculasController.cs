using Matriculas.Application;
using Microsoft.AspNetCore.Mvc;

namespace Matriculas.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MatriculasController : ControllerBase
{
    private readonly MatricularEstudianteHandler _handler;

    public MatriculasController(MatricularEstudianteHandler handler) => _handler = handler;

    [HttpPost]
    public async Task<IActionResult> Post(MatricularEstudianteCommand command, CancellationToken cancellationToken)
    {
        var id = await _handler.Handle(command, cancellationToken);
        return Ok(id);
    }
}
