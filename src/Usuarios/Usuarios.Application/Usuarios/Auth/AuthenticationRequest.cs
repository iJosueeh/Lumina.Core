namespace Usuarios.Application.Usuarios.Auth;

public sealed record AuthenticationRequest(string Email, string Password);