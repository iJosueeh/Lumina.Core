using System;
using System.Threading.Tasks;

namespace Estudiantes.Domain.Estudiantes;

public interface ICarritoRepository
{
    Task<Carrito?> GetByIdAsync(Guid estudianteId, CancellationToken cancellationToken = default);
    Task AddAsync(Carrito carrito);
    void Update(Carrito carrito);
}
