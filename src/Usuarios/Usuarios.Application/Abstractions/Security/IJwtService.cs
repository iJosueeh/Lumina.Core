using Usuarios.Domain.Usuarios;

namespace Usuarios.Application.Abstractions.Security;

public interface IJwtService
{
    string GenerateToken(UsuarioId usuarioId, NombreUsuario nombreUsuario, CorreoElectronico correo, string rolNombre);
}