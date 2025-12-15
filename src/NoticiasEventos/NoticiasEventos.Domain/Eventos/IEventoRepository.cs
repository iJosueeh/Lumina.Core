using NoticiasEventos.Domain.Eventos;

namespace NoticiasEventos.Domain.Eventos;

public interface IEventoRepository
{
    Task<List<Evento>> GetProximosAsync(CancellationToken cancellationToken = default);
    Task<Evento?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Evento evento, CancellationToken cancellationToken = default);
}
