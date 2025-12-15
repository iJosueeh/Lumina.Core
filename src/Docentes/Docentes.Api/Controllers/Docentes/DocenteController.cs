using Docentes.Application.Docentes.CrearDocente;
using Docentes.Application.Docentes.GetDocente;
using Docentes.Domain.Docentes;
using Docentes.Domain.Especialidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Docentes.api.Controllers.Docentes;

[ApiController]
[Route("api/docente")]
public class DocenteController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerDocente(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetDocenteQuery(new DocenteId(id));

        var resultado = await _sender.Send(query, cancellationToken);

        if (!resultado.IsSuccess)
            return NotFound(resultado.Error);

        return Ok(resultado.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CrearDocente(CrearDocenteRequest request, CancellationToken cancellationToken)
    {
        var command = new CrearDocenteCommand(
            request.UsuarioId,
            new EspecialidadId(request.EspecialidadId),
            request.Nombre,
            request.Cargo,
            request.Bio,
            request.Avatar,
            request.LinkedIn
        );

        var resultado = await _sender.Send(command, cancellationToken);

        if (!resultado.IsSuccess)
            return BadRequest(resultado.Error);

        return CreatedAtAction(
            nameof(ObtenerDocente),
            new { id = resultado.Value },
            new { Id = resultado.Value }
        );
    }
}