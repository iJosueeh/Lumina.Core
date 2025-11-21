using MediatR;
using Microsoft.AspNetCore.Mvc;
using Evaluaciones.Application.Evaluaciones.CrearEvaluacion;
using Evaluaciones.Application.Evaluaciones.ObtenerEvaluacion;

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
}