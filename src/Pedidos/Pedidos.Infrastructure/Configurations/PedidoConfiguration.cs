using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.Domain.Pedidos;

namespace Pedidos.Infrastructure.Configurations;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("pedidos");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(pedidoId => pedidoId.Value, value => new PedidoId(value));

        builder.Property(p => p.ClienteId);

        builder.Property(p => p.PrecioTotal)
            .HasColumnType("decimal(18,4)");

        builder.Property(p => p.Status)
            .HasConversion(status => status.ToString(),
                value => (PedidoStatus)Enum.Parse(typeof(PedidoStatus), value));

        builder.Property(p => p.FechaCreacion);

        builder.HasMany(p => p.PedidoItems)
            .WithOne()
            .HasForeignKey("PedidoId")
            .IsRequired();
    }
}