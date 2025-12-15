using Pedidos.Domain.Pedidos;

namespace Pedidos.Domain.Abstractions;

public interface IPedidoRepository
{
    Task<Pedido?> GetByIdAsync(PedidoId id, CancellationToken cancellationToken = default);

    void Add(Pedido pedido);
}