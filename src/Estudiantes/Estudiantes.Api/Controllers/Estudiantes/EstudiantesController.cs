using Estudiantes.Api.Atributos;
using Estudiantes.Application.Estudiantes.CrearEstudiante;
using Estudiantes.Application.Estudiantes.GetEstudiante;
using Estudiantes.Application.Estudiantes.GetDashboardStats;
using Estudiantes.Application.Estudiantes.GetCursosMatriculados;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Estudiantes.Api.Controllers.Estudiantes;

[ApiController]
[Route("api/estudiantes")]
public class EstudiantesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("{id}")]
    [RateLimit(5, 60)]
    public async Task<IActionResult> ObtenerEstudiante(
      Guid id,
      CancellationToken cancellationToken
    )
    {
        var query = new GetEstudianteQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);
        return resultado.IsSuccess ? Ok(resultado.Value) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CrearEstudiante(
       CrearEstudianteRequest request,
       CancellationToken cancellationToken
    )
    {
        var command = new CrearEstudianteCommand
        (
            request.UsuarioId
        );

        var resultado = await _sender.Send(command, cancellationToken);

        if (resultado.IsSuccess)
        {
            return CreatedAtAction(nameof(ObtenerEstudiante), new { id = resultado.Value }, resultado.Value);
        }
        return BadRequest(resultado.Error);
    }

    [HttpGet("{id}/dashboard-stats")]
    public async Task<IActionResult> ObtenerDashboardStats(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetDashboardStatsQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);
        return resultado.IsSuccess ? Ok(resultado.Value) : NotFound();
    }

    [HttpGet("{id}/cursos-matriculados")]
    public async Task<IActionResult> ObtenerCursosMatriculados(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetCursosMatriculadosQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);
        return resultado.IsSuccess ? Ok(resultado.Value) : NotFound();
    }
}