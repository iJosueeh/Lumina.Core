using MediatR;
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Application.Usuarios.DeleteUser;

internal sealed class DeleteUserCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(request.Id);

        if (usuario is null)
        {
            return Result.Failure(UsuarioErrores.NotFound);
        }

        _usuarioRepository.Delete(usuario);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}