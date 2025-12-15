using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Application.Pedidos.Crear;

namespace Pedidos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly ISender _sender;

    public PedidosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CrearPedido([FromBody] CrearPedidoCommand command)
    {
        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetPedido), new { id = result.Value }, result.Value);
    }

    // This is a placeholder for a future Get endpoint
    [HttpGet("{id}")]
    public IActionResult GetPedido(Guid id)
    {
        return Ok(new { PedidoId = id });
    }
}