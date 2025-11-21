using Evaluaciones.Domain.Evaluaciones;

namespace Evaluaciones.Infrastructure.Repositories;

internal sealed class EvaluacionRepository(ApplicationDbContext dbContext) : Repository<Evaluacion>(dbContext), IEvaluacionRepository
{
    public async Task<Evaluacion?> GetByIdAsync(EvaluacionId id, CancellationToken cancellationToken = default)
    {
        return await base.GetByIdAsync(id.Value, cancellationToken);
    }
}