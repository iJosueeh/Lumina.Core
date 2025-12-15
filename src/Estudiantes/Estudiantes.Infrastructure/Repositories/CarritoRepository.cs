using Estudiantes.Domain.Estudiantes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Estudiantes.Infrastructure.Repositories;

public class CarritoRepository : ICarritoRepository
{
    private readonly ApplicationDbContext _context;

    public CarritoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Carrito?> GetByIdAsync(Guid estudianteId, CancellationToken cancellationToken = default)
    {
        var carrito = await _context.Set<Carrito>().FirstOrDefaultAsync(c => c.Id == estudianteId, cancellationToken);

        if (carrito is null)
        {
            carrito = Carrito.Create(estudianteId);
            await AddAsync(carrito);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return carrito;
    }

    public async Task AddAsync(Carrito carrito)
    {
        await _context.Set<Carrito>().AddAsync(carrito);
    }

    public void Update(Carrito carrito)
    {
        _context.Set<Carrito>().Update(carrito);
    }
}
