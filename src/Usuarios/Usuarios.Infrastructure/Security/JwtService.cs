using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using Usuarios.Application.Abstractions.Security;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Infrastructure.Security;

public class JwtService(IConfiguration configuration) : IJwtService
{
    private readonly IConfiguration _configuration = configuration;

    public string GenerateToken(UsuarioId usuarioId, NombreUsuario nombreUsuario, CorreoElectronico correo, string rolNombre)
    {
        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]!);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuarioId.Value.ToString()),
            new Claim(ClaimTypes.Name, nombreUsuario.Value),
            new Claim(ClaimTypes.Email, correo.Value),
            new Claim(ClaimTypes.Role, rolNombre)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var encodedToken = tokenHandler.CreateToken(tokenDescriptor);
        return encodedToken;
    }
}