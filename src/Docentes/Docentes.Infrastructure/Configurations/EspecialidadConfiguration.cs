using Docentes.Domain.Especialidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docentes.Infrastructure.Configurations;

public class EspecialidadConfiguration : IEntityTypeConfiguration<Especialidad>
{
    public void Configure(EntityTypeBuilder<Especialidad> builder)
    {
        builder.ToTable("especialidades");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(
                especialidadId => especialidadId.Value,
                value => new EspecialidadId(value)
            )
            .ValueGeneratedNever()
            .HasColumnType("uuid");

        builder.Property(e => e.Nombre)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Descripcion)
            .HasMaxLength(500)
            .IsRequired();
    }
}