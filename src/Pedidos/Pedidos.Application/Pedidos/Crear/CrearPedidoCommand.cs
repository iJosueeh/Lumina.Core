using MediatR;
using Pedidos.Domain.Abstractions;

namespace Pedidos.Application.Pedidos.Crear;

public record CrearPedidoCommand(
    Guid ClienteId,
    List<PedidoItemCommand> Items) : IRequest<Result<Guid>>;
    
public record PedidoItemCommand(
    Guid CursoId);