using Usuarios.Application.Abstractions.Messaging;
using Usuarios.Application.Abstractions.Security;
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Application.Usuarios.Auth;

internal sealed class LoginCommandHandler(IUsuarioRepository usuarioRepository, IJwtService jwtService) : ICommandHandler<LoginCommand, AuthenticationResponse>
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<Result<AuthenticationResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var email = CorreoElectronico.Create(request.Email);
        if (email.IsFailure)
        {
            return Result.Failure<AuthenticationResponse>(email.Error);
        }

        var user = await _usuarioRepository.GetByEmailAsync(email.Value, cancellationToken);
        if (user is null)
        {
            return Result.Failure<AuthenticationResponse>(UserErrors.InvalidCredentials);
        }

        var password = Password.Create(request.Password);
        if (password.IsFailure)
        {
            return Result.Failure<AuthenticationResponse>(password.Error);
        }

        if (!user.Password!.Value.Equals(password.Value))
        {
            return Result.Failure<AuthenticationResponse>(UserErrors.InvalidCredentials);
        }
        
        var token = _jwtService.GenerateToken(new UsuarioId(user.Id), user.NombreUsuario!, user.CorreoElectronico!, user.Rol!.NombreRol!.Value);

        return new AuthenticationResponse(token, user.Rol.NombreRol!.Value);
    }
}