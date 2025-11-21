namespace Usuarios.Application.Usuarios.Auth;

public sealed record AuthenticationResponse(string Token, string RolPrincipal);