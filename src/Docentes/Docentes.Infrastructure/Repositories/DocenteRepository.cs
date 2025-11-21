using Docentes.Domain.Docentes;
using Microsoft.EntityFrameworkCore;

namespace Docentes.Infrastructure.Repositories;

internal sealed class DocenteRepository(ApplicationDbContext dbContext) : Repository<Docente, DocenteId>(dbContext), IDocenteRepository
{
    public new Task<Docente?> GetByIdAsync(
        DocenteId id,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(id, cancellationToken);
    }

    public async Task<Docente?> GetByIdUsuarioAsync(
        Guid idUsuario,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Docente>()
            .FirstOrDefaultAsync(
                docente => docente.UsuarioId == idUsuario,
                cancellationToken);
    }
}