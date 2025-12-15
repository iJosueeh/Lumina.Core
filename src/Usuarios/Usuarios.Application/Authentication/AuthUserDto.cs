namespace Usuarios.Application.Authentication;

public record AuthUserDto(
    Guid Id,
    string Email,
    string Nombre,
    string Apellido,
    string RolPrincipal
);
