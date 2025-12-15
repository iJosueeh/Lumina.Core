using Cursos.Application.Cursos;
using Cursos.Application.Cursos.CrearCurso;
using Cursos.Application.Cursos.GetAllCourses;
using Cursos.Application.Cursos.GetCategorias;
using Cursos.Application.Cursos.GetCourseById;
using Cursos.Application.Cursos.GetNiveles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cursos.Api.Controllers.Cursos;

[ApiController]
[Route("api/cursos")]
public class CursosController : ControllerBase
{
    private readonly ISender _sender;

    public CursosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CrearCurso
    (
        CrearCursoRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new CrearCursoCommand
        (
            request.Nombre!,
            request.Descripcion!,
            request.Capacidad,
            request.Nivel,
            request.Duracion,
            request.Precio,
            request.ImagenUrl,
            request.Categoria,
            request.InstructorId,
            request.Modulos?.Select(m => new ModuleDto(Guid.NewGuid(), m.Titulo, m.Descripcion, m.Lecciones)).ToList(),
            request.Requisitos
        );

        var result = await _sender.Send(command, cancellationToken);
        if (result.IsSuccess)
        {
            return CreatedAtAction(nameof(GetCourseById), new { id = result.Value }, result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCourses(CancellationToken cancellationToken)
    {
        var query = new GetAllCoursesQuery();
        var result = await _sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseById(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetCourseByIdQuery(id);
        var result = await _sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpGet("categorias")]
    public async Task<IActionResult> GetCategorias(CancellationToken cancellationToken)
    {
        var query = new GetCategoriasQuery();
        var categorias = await _sender.Send(query, cancellationToken);
        return Ok(categorias);
    }

    [HttpGet("niveles")]
    public async Task<IActionResult> GetNiveles(CancellationToken cancellationToken)
    {
        var query = new GetNivelesQuery();
        var niveles = await _sender.Send(query, cancellationToken);
        return Ok(niveles);
    }
}