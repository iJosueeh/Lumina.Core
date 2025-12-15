using Usuarios.Application.Authentication; // Add this using directive for AuthUserDto

namespace Usuarios.Application.Authentication;

public record AuthResponseDto(string Token, AuthUserDto UserInfo);