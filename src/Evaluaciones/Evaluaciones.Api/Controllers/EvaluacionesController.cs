using MediatR;
using Microsoft.AspNetCore.Mvc;
using Evaluaciones.Application.Evaluaciones.CrearEvaluacion;
using Evaluaciones.Application.Evaluaciones.ObtenerEvaluacion;
using Evaluaciones.Application.Evaluaciones.GetEvaluacionesByEstudiante;

namespace Evaluaciones.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EvaluacionesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CrearEvaluacion(
        [FromBody] CrearEvaluacionCommand command)
    {
        Guid evaluacionId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetEvaluacionById), new { id = evaluacionId }, evaluacionId);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvaluacionById(Guid id)
    {
        var query = new GetEvaluacionByIdQuery(id);
        var evaluacion = await _sender.Send(query);
        if (evaluacion == null)
        {
            return NotFound();
        }
        return Ok(evaluacion);
    }

    [HttpGet]
    public async Task<IActionResult> GetEvaluaciones(
        [FromQuery] Guid? estudianteId,
        [FromQuery] Guid? cursoId
    )
    {
        if (estudianteId.HasValue)
        {
            var query = new GetEvaluacionesByEstudianteQuery(estudianteId.Value);
            var evaluaciones = await _sender.Send(query);
            return Ok(evaluaciones);
        }

        // TODO: Implementar filtro por cursoId si es necesario
        return BadRequest("Se requiere estudianteId");
    }
}