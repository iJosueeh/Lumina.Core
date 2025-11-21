using Usuarios.Application.Abstractions.Security;
using Usuarios.Application.Abstractions.Services;
using Usuarios.Application.Abstractions.Time;
using Usuarios.Application.Usuarios;
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Roles;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Application.Services;

public class UserService : IUserService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IRolRepository _rolRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly NombreUsuarioService _nombreUsuarioService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UserService(
        IUsuarioRepository usuarioRepository,
        IRolRepository rolRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        NombreUsuarioService nombreUsuarioService,
        IDateTimeProvider dateTimeProvider)
    {
        _usuarioRepository = usuarioRepository;
        _rolRepository = rolRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _nombreUsuarioService = nombreUsuarioService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<IEnumerable<UserDto>>> GetAllAsync()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        var userDtos = new List<UserDto>();

        foreach (var usuario in usuarios)
        {
            userDtos.Add(new UserDto(
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
            ));
        }

        return userDtos;
    }

    public async Task<Result<UserDto>> GetByIdAsync(Guid id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
        {
            return Result.Failure<UserDto>(UsuarioErrores.NotFound);
        }


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

        return userDto;
    }

    public async Task<Result<Guid>> CreateAsync(UserManagementDto userDto)
    {
        var rol = await _rolRepository.GetByIdAsync(userDto.Rol);
        if (rol == null)
        {
            return Result.Failure<Guid>(RolErrores.NoEncontrado);
        }

        var hashedPassword = _passwordHasher.HashPassword(userDto.Password);
        var passwordResult = Password.Create(hashedPassword);
        if (passwordResult.IsFailure)
        {
            return Result.Failure<Guid>(passwordResult.Error);
        }

        var correoElectronicoResult = CorreoElectronico.Create(userDto.Email);
        if (correoElectronicoResult.IsFailure)
        {
            return Result.Failure<Guid>(correoElectronicoResult.Error);
        }

        var existingUser = await _usuarioRepository.GetByEmailAsync(correoElectronicoResult.Value);
        if (existingUser != null)
        {
            return Result.Failure<Guid>(UsuarioErrores.EmailYaExiste);
        }
        
        var usuario = Usuario.Create(
            new NombresPersona(userDto.Nombres),
            new ApellidoPaterno(userDto.ApellidoPaterno),
            new ApellidoMaterno(userDto.ApellidoMaterno),
            passwordResult.Value,
            userDto.FechaNacimiento.ToUniversalTime(),
            correoElectronicoResult.Value,
            new Direccion(
                userDto.Pais,
                userDto.Departamento,
                userDto.Provincia,
                userDto.Distrito,
                userDto.Calle
            ),
            rol.Id,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            _nombreUsuarioService
        );

        _usuarioRepository.Add(usuario);
        await _unitOfWork.SaveChangesAsync();

        return usuario.Id;
    }

    public async Task<Result> UpdateAsync(Guid id, UserManagementDto userDto)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
        {
            return Result.Failure(UsuarioErrores.NotFound);
        }

        var rol = await _rolRepository.GetByIdAsync(userDto.Rol);
        if (rol == null)
        {
            return Result.Failure(RolErrores.NoEncontrado);
        }

        usuario.GetType().GetProperty(nameof(Usuario.NombresPersona))?.SetValue(usuario, new NombresPersona(userDto.Nombres));
        usuario.GetType().GetProperty(nameof(Usuario.ApellidoPaterno))?.SetValue(usuario, new ApellidoPaterno(userDto.ApellidoPaterno));
        usuario.GetType().GetProperty(nameof(Usuario.ApellidoMaterno))?.SetValue(usuario, new ApellidoMaterno(userDto.ApellidoMaterno));
        usuario.GetType().GetProperty(nameof(Usuario.CorreoElectronico))?.SetValue(usuario, CorreoElectronico.Create(userDto.Email).Value);
        usuario.GetType().GetProperty(nameof(Usuario.FechaNacimiento))?.SetValue(usuario, userDto.FechaNacimiento.ToUniversalTime());

        usuario.GetType().GetProperty(nameof(Usuario.Direccion))?.SetValue(usuario, new Direccion(
                userDto.Pais,
                userDto.Departamento,
                userDto.Provincia,
                userDto.Distrito,
                userDto.Calle
            ));
        
        usuario.GetType().GetProperty(nameof(Usuario.RolId))?.SetValue(usuario, rol.Id);

        usuario.GetType().GetProperty(nameof(Usuario.FechaUltimoCambio))?.SetValue(usuario, _dateTimeProvider.CurrentTime.ToUniversalTime());

        _usuarioRepository.Update(usuario);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
        {
            return Result.Failure(UsuarioErrores.NotFound);
        }

        _usuarioRepository.Delete(usuario);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}