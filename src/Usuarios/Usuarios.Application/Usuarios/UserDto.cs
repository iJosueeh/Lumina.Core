namespace Usuarios.Application.Usuarios;

public record UserDto(
    Guid Id,
    string Username,
    string NombresPersona,
    string ApellidoPaterno,
    string ApellidoMaterno,
    DateTime FechaNacimiento,
    string Email,
    string Pais,
    string Departamento,
    string Provincia,
    string Distrito,
    string Calle,
    string Estado
);