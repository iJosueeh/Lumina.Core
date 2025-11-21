using Docentes.Domain.CursosImpartidos;

namespace Docentes.Infrastructure.Repositories;

internal sealed class CursoImpartidoRepository(ApplicationDbContext dbContext) : Repository<CursoImpartido, CursoImpartidoId>(dbContext), ICursoImpartidoRepository
{
    public Task<CursoImpartido?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
         => base.GetByIdAsync(new CursoImpartidoId(id), cancellationToken);
}