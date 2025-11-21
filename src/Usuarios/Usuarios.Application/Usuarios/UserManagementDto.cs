namespace Usuarios.Application.Usuarios;

public record UserManagementDto(
    Guid Id,
    string Password,
    Guid Rol,
    string Nombres,
    string ApellidoPaterno,
    string ApellidoMaterno,
    DateTime FechaNacimiento,
    string Email,
    string Pais,
    string Departamento,
    string Provincia,
    string Distrito,
    string Calle
);