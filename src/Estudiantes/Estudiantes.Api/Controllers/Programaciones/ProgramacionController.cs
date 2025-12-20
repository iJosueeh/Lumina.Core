using Estudiantes.Application.Programaciones.CrearProgramacion;
using Estudiantes.Application.Programaciones.GetProgramacion;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Estudiantes.Api.Controllers.Programaciones;

[ApiController]
[Route("api/programaciones")]
public class ProgramacionController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> ObtenerProgramacion(
        [FromQuery] Guid? estudianteId,
        [FromQuery] DateTime? hasta,
        [FromQuery] Guid? id,
        CancellationToken cancellationToken
    )
    {
        // Si viene ID específico, mantenemos comportamiento anterior
        if (id.HasValue)
        {
            var query = new GetProgramacionQuery(id.Value);
            var resultado = await _sender.Send(query, cancellationToken);
            return resultado.IsSuccess ? Ok(resultado.Value) : NotFound();
        }

        // Si es filtro para dashboard (estudianteId)
        if (estudianteId.HasValue)
        {
            var query = new GetProgramacionPorEstudianteQuery(estudianteId.Value);
            var resultado = await _sender.Send(query, cancellationToken);
            return resultado.IsSuccess ? Ok(resultado.Value) : BadRequest(resultado.Error);
        }

        return BadRequest("Faltan parámetros de filtro");
    }

    [HttpPost]
    public async Task<IActionResult> CrearProgramacion(
        CrearProgramacionRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new CrearProgramacionCommand
        (
          request.CursoId,
          request.DocenteId
        );

        var resultado = await _sender.Send(command, cancellationToken);

        if (resultado.IsSuccess)
        {
            return CreatedAtAction(nameof(ObtenerProgramacion), new { id = resultado.Value }, resultado.Value);
        }
        return BadRequest(resultado.Error);
    }
}