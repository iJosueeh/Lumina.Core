namespace Usuarios.Application.Authentication;

public record RegisterRequestDto(
    string Nombres,
    string ApellidoPaterno,
    string ApellidoMaterno,
    string Password,
    DateTime FechaNacimiento,
    string Correo,
    string Pais,
    string Departamento,
    string Provincia,
    string Distrito,
    string Calle
);
