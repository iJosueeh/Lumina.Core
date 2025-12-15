using Carreras.Application.Carreras.DTOs;
using Carreras.Application.Carreras.Queries.GetAllCarreras;
using Carreras.Application.Carreras.Queries.GetCarreraById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Carreras.Api.Controllers.Carreras;

[ApiController]
[Route("api/carreras")]
public class CarrerasController : ControllerBase
{
    private readonly ISender _sender;

    public CarrerasController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<List<CarreraDto>>> GetCarreras(CancellationToken cancellationToken)
    {
        var query = new GetAllCarrerasQuery();
        var result = await _sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarreraDto>> GetCarreraById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCarreraByIdQuery(id);
        var result = await _sender.Send(query, cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
