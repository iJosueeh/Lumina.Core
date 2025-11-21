using Usuarios.Application.Usuarios;

namespace Usuarios.Application.Authentication;

public record AuthResponseDto(string Token, UserDto UserInfo);
