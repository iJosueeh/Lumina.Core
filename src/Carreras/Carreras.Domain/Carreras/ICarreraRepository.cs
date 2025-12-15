namespace Carreras.Domain.Carreras;

public interface ICarreraRepository
{
    Task<List<Carrera>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Carrera?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Carrera>> GetByCategoriaAsync(string categoria, CancellationToken cancellationToken = default);
    Task AddAsync(Carrera carrera, CancellationToken cancellationToken = default);
    Task UpdateAsync(Carrera carrera, CancellationToken cancellationToken = default);
}
