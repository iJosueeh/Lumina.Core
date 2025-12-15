using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Estudiantes.Application.Carritos.AgregarCurso;
using Estudiantes.Application.Carritos.VerCarrito;
using Estudiantes.Domain.Abstractions;

namespace Estudiantes.Api.Controllers;

[ApiController]
[Route("api/carritos")]
public class CarritoController : ControllerBase
{
    private readonly ISender _sender;

    public CarritoController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{estudianteId}")]
    public async Task<IActionResult> VerCarrito(Guid estudianteId)
    {
        var query = new VerCarritoQuery(estudianteId);
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpPost("{estudianteId}/items")]
    public async Task<IActionResult> AgregarCursoAlCarrito(Guid estudianteId, [FromBody] AgregarCursoRequest request)
    {
        var command = new AgregarCursoAlCarritoCommand(estudianteId, request.CursoId);
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Created();
    }

    [HttpDelete("{estudianteId}/items/{cursoId}")]
    public async Task<IActionResult> EliminarCursoDelCarrito(Guid estudianteId, Guid cursoId)
    {
        var command = new Application.Carritos.EliminarCurso.EliminarCursoDelCarritoCommand(estudianteId, cursoId);
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return NoContent();
    }
}

public record AgregarCursoRequest(Guid CursoId);