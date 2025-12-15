using Carreras.Domain.Carreras;
using MongoDB.Driver;

namespace Carreras.Infrastructure.Repositories;

public class CarreraRepository : Repository<Carrera>, ICarreraRepository
{
    public CarreraRepository(IMongoCollection<Carrera> collection) : base(collection)
    {
    }

    public async Task<List<Carrera>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(_ => true).ToListAsync(cancellationToken);
    }

    public new async Task<Carrera?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await base.GetByIdAsync(id, cancellationToken);
    }

    public async Task<List<Carrera>> GetByCategoriaAsync(string categoria, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Carrera>.Filter.Eq(c => c.Categoria, categoria);
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Carrera carrera, CancellationToken cancellationToken = default)
    {
        await base.AddAsync(carrera, cancellationToken);
    }

    public async Task UpdateAsync(Carrera carrera, CancellationToken cancellationToken = default)
    {
        await base.UpdateAsync(carrera.Id, carrera, cancellationToken);
    }
}
