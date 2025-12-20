using Evaluaciones.Domain.Evaluaciones;
using Microsoft.EntityFrameworkCore;

namespace Evaluaciones.Infrastructure.Repositories;

internal sealed class EvaluacionRepository(ApplicationDbContext dbContext) : Repository<Evaluacion>(dbContext), IEvaluacionRepository
{
    public async Task<Evaluacion?> GetByIdAsync(EvaluacionId id, CancellationToken cancellationToken = default)
    {
        return await base.GetByIdAsync(id.Value, cancellationToken);
    }

    public async Task<List<Evaluacion>> GetByCursoIdsAsync(List<Guid> cursoIds, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Evaluacion>()
            .Where(e => cursoIds.Contains(e.CursoId))
            .ToListAsync(cancellationToken);
    }
}