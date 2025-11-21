using Usuarios.Application.Abstractions.Security;
using Usuarios.Application.Abstractions.Services;
using Usuarios.Application.Abstractions.Time;
using Usuarios.Application.Authentication;
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Roles;
using Usuarios.Domain.Usuarios;
using Usuarios.Application.Usuarios;

namespace Usuarios.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtService _jwtService;
    private readonly IRolRepository _rolRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly NombreUsuarioService _nombreUsuarioService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        IUsuarioRepository usuarioRepository,
        IJwtService jwtService,
        IRolRepository rolRepository,
        IDateTimeProvider dateTimeProvider,
        NombreUsuarioService nombreUsuarioService,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _jwtService = jwtService;
        _rolRepository = rolRepository;
        _dateTimeProvider = dateTimeProvider;
        _nombreUsuarioService = nombreUsuarioService;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<AuthResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var correoElectronico = CorreoElectronico.Create(request.Email);
        if (correoElectronico.IsFailure)
        {
            return Result.Failure<AuthResponseDto>(correoElectronico.Error);
        }

        var usuario = await _usuarioRepository.GetByEmailAsync(correoElectronico.Value);
        if (usuario == null)
        {
            return Result.Failure<AuthResponseDto>(UsuarioErrores.CredencialesInvalidas);
        }

        if (usuario.Password == null || !_passwordHasher.VerifyPassword(usuario.Password.Value, request.Password))
        {
            return Result.Failure<AuthResponseDto>(UsuarioErrores.CredencialesInvalidas);
        }

        var rol = await _rolRepository.GetByIdAsync(usuario.RolId);
        if (rol == null || rol.NombreRol == null)
        {
            return Result.Failure<AuthResponseDto>(RolErrores.NoEncontrado);
        }

        var token = _jwtService.GenerateToken(
            new UsuarioId(usuario.Id),
            usuario.NombreUsuario!,
            usuario.CorreoElectronico!,
            rol.NombreRol.Value
        );

        var userDto = new UserDto(
            usuario.Id,
            usuario.NombreUsuario!.Value,
            usuario.NombresPersona!.Value,
            usuario.ApellidoPaterno!.Value,
            usuario.ApellidoMaterno!.Value,
            usuario.FechaNacimiento,
            usuario.CorreoElectronico!.Value,
            usuario.Direccion!.Pais!,
            usuario.Direccion.Departamento!,
            usuario.Direccion.Provincia!,
            usuario.Direccion.Distrito!,
            usuario.Direccion.Calle!,
            usuario.Estado.ToString()
        );

        return new AuthResponseDto(token, userDto);
    }

    public async Task<Result<AuthResponseDto>> RegisterAsync(RegisterRequestDto request)
    {
        var rolDefault = await _rolRepository.GetByNameAsync("Student");
        if (rolDefault == null)
        {
            return Result.Failure<AuthResponseDto>(RolErrores.NoEncontrado);
        }

        var hashedPassword = _passwordHasher.HashPassword(request.Password);
        var passwordResult = Password.Create(hashedPassword);
        if (passwordResult.IsFailure)
        {
            return Result.Failure<AuthResponseDto>(passwordResult.Error);
        }

        var correoElectronicoResult = CorreoElectronico.Create(request.Correo);
        if (correoElectronicoResult.IsFailure)
        {
            return Result.Failure<AuthResponseDto>(correoElectronicoResult.Error);
        }

        var existingUser = await _usuarioRepository.GetByEmailAsync(correoElectronicoResult.Value);
        if (existingUser != null)
        {
            return Result.Failure<AuthResponseDto>(UsuarioErrores.EmailYaExiste);
        }

        var usuario = Usuario.Create(
            new NombresPersona(request.Nombres),
            new ApellidoPaterno(request.ApellidoPaterno),
            new ApellidoMaterno(request.ApellidoMaterno),
            passwordResult.Value,
            request.FechaNacimiento.ToUniversalTime(),
            correoElectronicoResult.Value,
            new Direccion(
                request.Pais,
                request.Departamento,
                request.Provincia,
                request.Distrito,
                request.Calle
            ),
            rolDefault.Id,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            _nombreUsuarioService
        );

        _usuarioRepository.Add(usuario);
        await _unitOfWork.SaveChangesAsync();

        var rol = await _rolRepository.GetByIdAsync(usuario.RolId);
        if (rol == null || rol.NombreRol == null)
        {
            return Result.Failure<AuthResponseDto>(RolErrores.NoEncontrado);
        }

        var token = _jwtService.GenerateToken(
            new UsuarioId(usuario.Id),
            usuario.NombreUsuario!,
            usuario.CorreoElectronico!,
            rol.NombreRol.Value
        );

        var userDto = new UserDto(
            usuario.Id,
            usuario.NombreUsuario!.Value,
            usuario.NombresPersona!.Value,
            usuario.ApellidoPaterno!.Value,
            usuario.ApellidoMaterno!.Value,
            usuario.FechaNacimiento,
            usuario.CorreoElectronico!.Value,
            usuario.Direccion!.Pais!,
            usuario.Direccion.Departamento!,
            usuario.Direccion.Provincia!,
            usuario.Direccion.Distrito!,
            usuario.Direccion.Calle!,
            usuario.Estado.ToString()
        );

        return new AuthResponseDto(token, userDto);
    }
}
