namespace Usuarios.Domain.Roles;

public interface IRolRepository
{
    Task<Rol?> GetByNameAsync(string rol, CancellationToken cancellationToken = default );
    Task<Rol?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}