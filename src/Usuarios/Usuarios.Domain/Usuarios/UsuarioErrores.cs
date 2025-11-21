using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public class UsuarioErrores
{
    public static Error YaSeEncuentraActivo = new (
        "Usuario.YaSeEncuentraActivo",
        "El usuario ya se encuentra activo"
    );

    public static Error YaSeEncuentraInactivo = new (
        "Usuario.YaSeEncuentraInactivo",
        "El usuario ya se encuentra inactivo"
    );

    public static Error NotFound = new (
        "Usuario.NotFound",
        "No se encontró el usuario."
    );

    public static Error EmailYaExiste = new (
        "Usuario.EmailYaExiste",
        "El correo electrónico ya está registrado."
    );

    public static Error CredencialesInvalidas = new (
        "Usuario.CredencialesInvalidas",
        "Credenciales inválidas."
    );
}