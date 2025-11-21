using Microsoft.EntityFrameworkCore;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Infrastructure.Repositories;

internal sealed class UsuarioRepository(ApplicationDbContext dbContext) : Repository<Usuario>(dbContext), IUsuarioRepository
{
    public async Task<bool> ExisteCorreoAsync(
        CorreoElectronico correo,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Usuario>()
        .AnyAsync(usuario => ((string)usuario.CorreoElectronico!) == correo.Value,
        cancellationToken);
    }

    public async Task<Usuario?> GetByEmailAsync(CorreoElectronico email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Usuario>()
            .Include(usuario => usuario.Rol)
            .FirstOrDefaultAsync(usuario => ((string)usuario.CorreoElectronico!) == email.Value, cancellationToken);
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _dbContext.Set<Usuario>()
            .Include(usuario => usuario.Rol)
            .ToListAsync();
    }

    public new void Update(Usuario usuario)
    {
        _dbContext.Set<Usuario>().Update(usuario);
    }
}