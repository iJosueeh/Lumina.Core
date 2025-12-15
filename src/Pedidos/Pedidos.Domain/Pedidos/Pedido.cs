using Pedidos.Domain.Abstractions;

namespace Pedidos.Domain.Pedidos;

public class Pedido : Entity<PedidoId>, IAggregateRoot
{
    private readonly List<PedidoItem> _pedidoItems = new();

    private Pedido(
        PedidoId id,
        Guid clienteId,
        decimal precioTotal,
        PedidoStatus status,
        DateTime fechaCreacion) : base(id)
    {
        ClienteId = clienteId;
        PrecioTotal = precioTotal;
        Status = status;
        FechaCreacion = fechaCreacion;
    }

    private Pedido() { }

    public Guid ClienteId { get; private set; }
    public decimal PrecioTotal { get; private set; }
    public PedidoStatus Status { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public IReadOnlyList<PedidoItem> PedidoItems => _pedidoItems.AsReadOnly();

    public static Pedido Crear(
        Guid clienteId,
        List<(Guid CursoId, string NombreCurso, decimal Precio)> items)
    {
        var pedidoId = new PedidoId(Guid.NewGuid());
        var precioTotal = items.Sum(i => i.Precio);
        
        var pedido = new Pedido(
            pedidoId,
            clienteId,
            precioTotal,
            PedidoStatus.Pendiente,
            DateTime.UtcNow);

        foreach (var item in items)
        {
            pedido.AddPedidoItem(item.CursoId, item.NombreCurso, item.Precio);
        }

        return pedido;
    }

    private void AddPedidoItem(Guid cursoId, string nombreCurso, decimal precio)
    {
        var pedidoItem = new PedidoItem(
            new PedidoItemId(Guid.NewGuid()),
            Id,
            cursoId,
            nombreCurso,
            precio);
        
        _pedidoItems.Add(pedidoItem);
    }
}