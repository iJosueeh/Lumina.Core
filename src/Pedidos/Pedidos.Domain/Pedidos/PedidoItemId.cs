using Pedidos.Domain.Abstractions;

namespace Pedidos.Domain.Pedidos;

public record PedidoItemId(Guid Value) : IStronglyTypedId;