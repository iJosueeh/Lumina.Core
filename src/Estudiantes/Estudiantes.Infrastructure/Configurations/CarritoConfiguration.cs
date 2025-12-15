using Estudiantes.Domain.Estudiantes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estudiantes.Infrastructure.Configurations;

public class CarritoConfiguration : IEntityTypeConfiguration<Carrito>
{
    public void Configure(EntityTypeBuilder<Carrito> builder)
    {
        builder.ToTable("carritos");
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.CursoIds)
            .HasColumnType("jsonb");
    }
}
