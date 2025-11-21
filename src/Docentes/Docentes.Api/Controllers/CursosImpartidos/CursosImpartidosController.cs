using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Docentes.api.Controllers.CursosImpartidos;

[ApiController]
[Route("api/cursosImpartidos")]
public class CursosImpartidosController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
}