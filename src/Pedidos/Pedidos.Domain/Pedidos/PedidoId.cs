using Pedidos.Domain.Abstractions;

namespace Pedidos.Domain.Pedidos;

public record PedidoId(Guid Value) : IStronglyTypedId;