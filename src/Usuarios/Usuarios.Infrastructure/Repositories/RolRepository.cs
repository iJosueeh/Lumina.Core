using Microsoft.EntityFrameworkCore;
using Usuarios.Domain.Roles;
using Microsoft.Extensions.Logging;

namespace Usuarios.Infrastructure.Repositories;

internal sealed class RolRepository(ApplicationDbContext dbContext, ILogger<RolRepository> logger) : Repository<Rol>(dbContext), IRolRepository
{
    private readonly ILogger<RolRepository> _logger = logger;

    public Task<Rol?> GetByNameAsync(string rolNombre, CancellationToken cancellationToken = default) 
    {
        _logger.LogInformation("Searching for role with name: {RolNombre}", rolNombre);

        return Task.FromResult(_dbContext.Set<Rol>().AsEnumerable().FirstOrDefault(
            r => {
                _logger.LogInformation("Comparing with role in DB: {DbRolName}", r.NombreRol?.Value);
                return r.NombreRol != null && r.NombreRol.Value == rolNombre;
            }
        ));
    }

    public new async Task<Rol?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Rol>().FirstOrDefaultAsync(rol => rol.Id == id, cancellationToken);
    }
}