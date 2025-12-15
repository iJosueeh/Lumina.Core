using MongoDB.Driver;
using NoticiasEventos.Domain.Noticias;

namespace NoticiasEventos.Infrastructure.Repositories;

public class NoticiaRepository : INoticiaRepository
{
    private readonly IMongoCollection<Noticia> _noticias;

    public NoticiaRepository(IMongoDatabase database)
    {
        _noticias = database.GetCollection<Noticia>("noticias");
    }

    public async Task<List<Noticia>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _noticias.Find(n => true).ToListAsync(cancellationToken);
    }

    public async Task<Noticia?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Noticia>.Filter.Eq(n => n.Id, id);
        return await _noticias.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(Noticia noticia, CancellationToken cancellationToken = default)
    {
        await _noticias.InsertOneAsync(noticia, cancellationToken: cancellationToken);
    }
}
