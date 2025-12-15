using MongoDB.Driver;
using NoticiasEventos.Domain.Eventos;

namespace NoticiasEventos.Infrastructure.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly IMongoCollection<Evento> _eventos;

    public EventoRepository(IMongoDatabase database)
    {
        _eventos = database.GetCollection<Evento>("eventos");
    }

    public async Task<List<Evento>> GetProximosAsync(CancellationToken cancellationToken = default)
    {
        var filter = Builders<Evento>.Filter.Eq(e => e.EsProximo, true);
        var sort = Builders<Evento>.Sort.Ascending(e => e.Fecha); // Opcional: ordenar por fecha
        return await _eventos.Find(filter).Sort(sort).ToListAsync(cancellationToken);
    }

    public async Task<Evento?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Evento>.Filter.Eq(e => e.Id, id);
        return await _eventos.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(Evento evento, CancellationToken cancellationToken = default)
    {
        await _eventos.InsertOneAsync(evento, cancellationToken: cancellationToken);
    }
}
