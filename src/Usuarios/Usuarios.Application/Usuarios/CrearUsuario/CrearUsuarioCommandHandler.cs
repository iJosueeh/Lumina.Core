using Usuarios.Application.Abstractions.Messaging;
using Usuarios.Application.Abstractions.Time;
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Roles;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Application.Usuarios.CrearUsuario;

internal sealed class CrearUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, NombreUsuarioService nombreUsuarioService, IDateTimeProvider dateTimeProvider, IRolRepository rolRepository) :
ICommandHandler<CrearUsuarioCommand, Guid>
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly NombreUsuarioService _nombreUsuarioService = nombreUsuarioService;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly IRolRepository _rolRepository = rolRepository;

    public async Task<Result<Guid>> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
    {
        string roleNameToSearch = request.RolNombre;

        var rolObtenido = await _rolRepository.GetByNameAsync(roleNameToSearch, cancellationToken);

        if (rolObtenido == null)
        {
            return Result.Failure<Guid>(RolErrores.NoEncontrado);
        }

        var passwordResult = Password.Create(request.Password);
        if (passwordResult.IsFailure)
        {
            return Result.Failure<Guid>(passwordResult.Error);
        }

        var usuario = Usuario.Create(
            new NombresPersona(request.Nombres),
            new ApellidoPaterno(request.ApellidoPaterno),
            new ApellidoMaterno(request.ApellidoMaterno),
            passwordResult.Value,
            request.FechaNacimiento.ToUniversalTime(),
            CorreoElectronico.Create(request.Correo).Value,
            new Direccion(
                request.Pais,
                request.Departamento,
                request.Provincia,
                request.Distrito,
                request.Calle
            ),
            rolObtenido.Id,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            _nombreUsuarioService
        );

        _usuarioRepository.Add(usuario);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return usuario.Id;
    }
}