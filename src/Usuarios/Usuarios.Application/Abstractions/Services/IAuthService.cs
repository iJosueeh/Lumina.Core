using Usuarios.Application.Authentication;
using Usuarios.Domain.Abstractions;

namespace Usuarios.Application.Abstractions.Services;

public interface IAuthService
{
    Task<Result<AuthResponseDto>> LoginAsync(LoginRequestDto request);
    Task<Result<AuthResponseDto>> RegisterAsync(RegisterRequestDto request);
}
