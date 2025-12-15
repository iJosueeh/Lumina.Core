using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Abstractions;
using Pedidos.Domain.Pedidos;

namespace Pedidos.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly ApplicationDbContext _context;

    public PedidoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
    }

    public async Task<Pedido?> GetByIdAsync(PedidoId id, CancellationToken cancellationToken = default)
    {
        return await _context.Pedidos
            .Include(p => p.PedidoItems)
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}