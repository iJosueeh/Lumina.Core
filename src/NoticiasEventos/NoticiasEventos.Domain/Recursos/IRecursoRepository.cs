using NoticiasEventos.Domain.Recursos;

namespace NoticiasEventos.Domain.Recursos;

public interface IRecursoRepository
{
    Task<Recurso?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Recurso>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<Recurso>> GetDestacadosAsync(int limit, CancellationToken cancellationToken = default);
    void Add(Recurso recurso);
    void Delete(Recurso recurso);
}
