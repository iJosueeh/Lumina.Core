using Evaluaciones.Domain.Evaluaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evaluaciones.Infrastructure.Configurations;

public class EvaluacionConfiguration : IEntityTypeConfiguration<Evaluacion>
{
    public void Configure(EntityTypeBuilder<Evaluacion> builder)
    {
        builder.ToTable("Evaluaciones");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(evaluacionId => evaluacionId.Value, value => new EvaluacionId(value));

        builder.Property(e => e.CursoId)
            .IsRequired();

        builder.Property(e => e.DocenteId)
            .IsRequired();

        builder.Property(e => e.Titulo)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.Descripcion)
            .HasMaxLength(1000);

        builder.Property(e => e.FechaCreacion)
            .IsRequired();

        builder.Property(e => e.FechaInicio)
            .IsRequired();

        builder.Property(e => e.FechaFin)
            .IsRequired();

        builder.Property(e => e.PuntajeMaximo)
            .HasColumnType("decimal(5,2)")
            .IsRequired();

        builder.Property(e => e.TipoEvaluacion)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(e => e.Estado)
            .HasConversion<string>()
            .IsRequired();

        builder.Property<uint>("Version").IsRowVersion();
    }
}