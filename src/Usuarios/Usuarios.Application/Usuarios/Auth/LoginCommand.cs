using Usuarios.Application.Abstractions.Messaging;

namespace Usuarios.Application.Usuarios.Auth;

public sealed record LoginCommand(string Email, string Password) : ICommand<AuthenticationResponse>;