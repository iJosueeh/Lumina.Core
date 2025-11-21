using MediatR;
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Application.Usuarios.GetAllUsers;

internal sealed class GetAllUsersQueryHandler(IUsuarioRepository usuarioRepository) : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserDto>>>
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

    public async Task<Result<IEnumerable<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var usuarios = await _usuarioRepository.GetAllAsync();

        if (usuarios is null || !usuarios.Any())
        {
            return Result.Failure<IEnumerable<UserDto>>(UsuarioErrores.NotFound);
        }

        var usersDto = usuarios.Select(usuario => new UserDto(
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
        )).ToList();

        return Result.Success<IEnumerable<UserDto>>(usersDto);
    }
}