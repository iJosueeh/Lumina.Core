using MediatR;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Usuarios.CrearUsuario;
using Usuarios.Application.Usuarios.GetUsuario;
using Usuarios.Application.Usuarios.GetAllUsers;
using Usuarios.Application.Usuarios.UpdateUser;
using Usuarios.Application.Usuarios.DeleteUser;
using Microsoft.AspNetCore.Authorization;
using Usuarios.Application.Usuarios;
using Usuarios.Domain.Abstractions;

namespace Usuarios.Api.Controllers.Usuarios;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController(ISender sender, IConfiguration configuration) : ControllerBase
{

     private readonly ISender _sender = sender;
     private readonly IConfiguration _configuration = configuration;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(
        Guid id,
        CancellationToken cancellationToken
        )
    {
        var query = new GetUsuarioQuery(id);
        var user = await _sender.Send(query,cancellationToken);
        return user is not null ? Ok(user) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(
        CrearUsuarioRequest  request,
        CancellationToken cancellationToken)
    {
        var command = new CrearUsuarioCommand(
            request.Password,
            request.Rol,
            request.ApellidoPaterno,
            request.ApellidoMaterno,
            request.Nombres,
            request.FechaNacimiento,
            request.CorreoElectronico,
            request.Pais,
            request.Departamento,
            request.Provincia,
            request.Distrito,
            request.Calle
        );
        var resultado = await _sender.Send(command,cancellationToken);

        if(resultado.IsSuccess)
        {
         return CreatedAtAction(nameof(GetUser), new { id = resultado.Value }, resultado.Value);
        }
        return BadRequest(resultado.Error);

    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllUsersQuery();
        var result = await _sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, UserManagementDto request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest(new Error("User.IdMismatch", "The ID in the URL does not match the ID in the request body."));
        }

        var command = new UpdateUserCommand(
            request.Id,
            request.Password,
            request.Rol,
            request.ApellidoPaterno,
            request.ApellidoMaterno,
            request.Nombres,
            request.FechaNacimiento,
            request.Email,
            request.Pais,
            request.Departamento,
            request.Provincia,
            request.Distrito,
            request.Calle
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(id);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return NoContent();
    }
}