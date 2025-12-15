using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.Domain.Pedidos;

namespace Pedidos.Infrastructure.Configurations;

public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
        builder.ToTable("pedido_items");

        builder.HasKey(pi => pi.Id);
        builder.Property(pi => pi.Id)
            .HasConversion(pedidoItemId => pedidoItemId.Value, value => new PedidoItemId(value));

        builder.Property(pi => pi.PedidoId)
            .HasConversion(pedidoId => pedidoId.Value, value => new PedidoId(value));
            
        builder.Property(pi => pi.CursoId);

        builder.Property(pi => pi.NombreCurso)
            .HasMaxLength(200);

        builder.Property(pi => pi.Precio)
            .HasColumnType("decimal(18,4)");
    }
}