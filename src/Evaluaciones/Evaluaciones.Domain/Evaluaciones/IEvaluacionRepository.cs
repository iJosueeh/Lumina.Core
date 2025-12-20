namespace Evaluaciones.Domain.Evaluaciones;

public interface IEvaluacionRepository
{
    Task<Evaluacion?> GetByIdAsync(EvaluacionId id, CancellationToken cancellationToken = default);
    Task<List<Evaluacion>> GetByCursoIdsAsync(List<Guid> cursoIds, CancellationToken cancellationToken = default);
    void Add(Evaluacion evaluacion);
}