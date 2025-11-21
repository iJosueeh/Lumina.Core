using Docentes.Application.Especialidades.CrearEspecialidad;
using Docentes.Application.Especialidades.ObtenerEspecialidad;
using Docentes.Domain.Especialidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Docentes.Api.Controllers.Especialidad;

[ApiController]
[Route("api/especialidades")]
public class EspecialidadesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CrearEspecialidad(
        [FromBody] CrearEspecialidadCommand command,
        CancellationToken cancellationToken)
    {
        var resultado = await _sender.Send(command, cancellationToken);

        if (!resultado.IsSuccess)
            return BadRequest(resultado.Error);

        return CreatedAtAction(
            nameof(GetEspecialidadById),
            new { id = resultado.Value },
            new { Id = resultado.Value }
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEspecialidadById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetEspecialidadByIdQuery(new EspecialidadId(id));
        var resultado = await _sender.Send(query, cancellationToken);

        if (!resultado.IsSuccess)
            return NotFound(resultado.Error);

        return Ok(resultado.Value);
    }
}