using Pedidos.Domain.Abstractions;

namespace Pedidos.Domain.Pedidos;

public class PedidoItem : Entity<PedidoItemId>
{
    public PedidoItem(
        PedidoItemId id,
        PedidoId pedidoId,
        Guid cursoId,
        string nombreCurso,
        decimal precio) : base(id)
    {
        PedidoId = pedidoId;
        CursoId = cursoId;
        NombreCurso = nombreCurso;
        Precio = precio;
    }

    private PedidoItem() { }

    public PedidoId PedidoId { get; private set; }
    public Guid CursoId { get; private set; }
    public string NombreCurso { get; private set; }
    public decimal Precio { get; private set; }
}