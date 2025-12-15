using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Estudiantes.Application.Estudiantes.CrearInscripcion;

namespace Estudiantes.Api.Controllers;

[ApiController]
[Route("api/estudiantes/inscripciones")]
public class InscripcionesController : ControllerBase
{
    private readonly ISender _sender;

    public InscripcionesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CrearInscripcion([FromBody] CrearInscripcionRequest request)
    {
        var command = new CrearInscripcionCommand(request.EstudianteId, request.CursoId);
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Created();
    }
}

public record CrearInscripcionRequest(Guid EstudianteId, Guid CursoId);
