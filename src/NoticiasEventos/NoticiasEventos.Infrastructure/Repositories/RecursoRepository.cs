using MongoDB.Driver;
using NoticiasEventos.Domain.Recursos;

namespace NoticiasEventos.Infrastructure.Repositories;

public class RecursoRepository : IRecursoRepository
{
    private readonly IMongoCollection<Recurso> _recursos;

    public RecursoRepository(IMongoDatabase database)
    {
        _recursos = database.GetCollection<Recurso>("recursos");
    }

    public async Task<List<Recurso>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _recursos.Find(r => true).ToListAsync(cancellationToken);
    }

    public async Task<List<Recurso>> GetDestacadosAsync(int limit, CancellationToken cancellationToken = default)
    {
        return await _recursos.Find(r => r.EsDestacado)
            .SortByDescending(r => r.FechaPublicacion)
            .Limit(limit)
            .ToListAsync(cancellationToken);
    }

    public async Task<Recurso?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Recurso>.Filter.Eq(r => r.Id, id);
        return await _recursos.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public void Add(Recurso recurso)
    {
        _recursos.InsertOne(recurso);
    }

    public void Delete(Recurso recurso)
    {
        _recursos.DeleteOne(r => r.Id == recurso.Id);
    }
}
