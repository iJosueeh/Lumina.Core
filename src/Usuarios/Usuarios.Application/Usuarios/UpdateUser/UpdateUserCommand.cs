using MediatR;
using Usuarios.Domain.Abstractions;

namespace Usuarios.Application.Usuarios.UpdateUser;

public record UpdateUserCommand(
    Guid Id,
    string Password,
    Guid Rol,
    string ApellidoPaterno,
    string ApellidoMaterno,
    string Nombres,
    DateTime FechaNacimiento,
    string CorreoElectronico,
    string Pais,
    string Departamento,
    string Provincia,
    string Distrito,
    string Calle
) : IRequest<Result>;