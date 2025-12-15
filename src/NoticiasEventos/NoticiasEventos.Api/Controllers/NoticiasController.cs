using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoticiasEventos.Application.Noticias.Queries;

namespace NoticiasEventos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoticiasController : ControllerBase
{
    private readonly ISender _sender;

    public NoticiasController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetNoticias(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? categoria = null,
        [FromQuery] string? search = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetNoticiasQuery(page, pageSize, categoria, search);
        var noticias = await _sender.Send(query, cancellationToken);
        return Ok(noticias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNoticiaById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetNoticiaByIdQuery(id);
        var noticia = await _sender.Send(query, cancellationToken);
        
        if (noticia == null)
            return NotFound();

        return Ok(noticia);
    }

    [HttpGet("categorias")]
    public async Task<IActionResult> GetCategorias(CancellationToken cancellationToken)
    {
        var query = new GetCategoriasQuery();
        var categorias = await _sender.Send(query, cancellationToken);
        return Ok(categorias);
    }
}
