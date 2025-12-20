using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoticiasEventos.Application.Recursos.Queries;

namespace NoticiasEventos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecursosController : ControllerBase
{
    private readonly ISender _sender;

    public RecursosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetRecursos(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? categoria = null,
        [FromQuery] string? tipo = null,
        [FromQuery] string? search = null,
        [FromQuery] bool? destacado = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetRecursosQuery(search, categoria, tipo, destacado, page, pageSize);
        var recursos = await _sender.Send(query, cancellationToken);
        return Ok(recursos);
    }
}
