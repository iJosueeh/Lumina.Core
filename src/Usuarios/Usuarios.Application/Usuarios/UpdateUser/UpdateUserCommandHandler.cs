using MediatR;
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Roles;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Application.Usuarios.UpdateUser;

internal sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IRolRepository _rolRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(
        IUsuarioRepository usuarioRepository,
        IRolRepository rolRepository,
        IUnitOfWork unitOfWork)
    {
        _usuarioRepository = usuarioRepository;
        _rolRepository = rolRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(request.Id);

        if (usuario is null)
        {
            return Result.Failure(UsuarioErrores.NotFound);
        }

        if (request.Rol != Guid.Empty && request.Rol != usuario.RolId)
        {
            var rol = await _rolRepository.GetByIdAsync(request.Rol);
            if (rol is null)
            {
                return Result.Failure(RolErrores.NoEncontrado);
            }
        }

        usuario.GetType().GetProperty(nameof(Usuario.NombresPersona))?.SetValue(usuario, new NombresPersona(request.Nombres));
        usuario.GetType().GetProperty(nameof(Usuario.ApellidoPaterno))?.SetValue(usuario, new ApellidoPaterno(request.ApellidoPaterno));
        usuario.GetType().GetProperty(nameof(Usuario.ApellidoMaterno))?.SetValue(usuario, new ApellidoMaterno(request.ApellidoMaterno));
        usuario.GetType().GetProperty(nameof(Usuario.FechaNacimiento))?.SetValue(usuario, request.FechaNacimiento);
        usuario.GetType().GetProperty(nameof(Usuario.CorreoElectronico))?.SetValue(usuario, CorreoElectronico.Create(request.CorreoElectronico).Value);
        usuario.GetType().GetProperty(nameof(Usuario.Password))?.SetValue(usuario, Password.Create(request.Password).Value);
        
        usuario.GetType().GetProperty(nameof(Usuario.Direccion))?.SetValue(usuario, new Direccion(
            request.Pais,
            request.Departamento,
            request.Provincia,
            request.Distrito,
            request.Calle
        ));

        usuario.GetType().GetProperty(nameof(Usuario.RolId))?.SetValue(usuario, request.Rol);
        usuario.GetType().GetProperty(nameof(Usuario.FechaUltimoCambio))?.SetValue(usuario, DateTime.UtcNow);


        _usuarioRepository.Update(usuario);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}