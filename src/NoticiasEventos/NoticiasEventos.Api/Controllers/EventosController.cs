using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoticiasEventos.Application.Eventos.Queries;

namespace NoticiasEventos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly ISender _sender;

    public EventosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("proximos")]
    public async Task<IActionResult> GetEventosProximos(CancellationToken cancellationToken)
    {
        var query = new GetEventosProximosQuery();
        var eventos = await _sender.Send(query, cancellationToken);
        return Ok(eventos);
    }
}
