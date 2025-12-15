using NoticiasEventos.Domain.Noticias;

namespace NoticiasEventos.Domain.Noticias;

public interface INoticiaRepository
{
    Task<Noticia?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Noticia>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Noticia noticia, CancellationToken cancellationToken = default);
}
