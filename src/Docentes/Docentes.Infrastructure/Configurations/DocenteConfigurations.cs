using Docentes.Domain.Docentes;
using Docentes.Domain.Especialidades;
using Docentes.Infrastructure.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docentes.Infrastructure.Configurations;

public class DocenteConfigurations : IEntityTypeConfiguration<Docente>
{
    public void Configure(EntityTypeBuilder<Docente> builder)
    {
        builder.ToTable("docentes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(new StronglyTypedIdConverter<DocenteId>())
            .ValueGeneratedNever();

        builder.Property(x => x.UsuarioId)
            .IsRequired();

        builder.HasIndex(x => x.UsuarioId)
            .IsUnique();

        builder.Property(x => x.EspecialidadId)
            .HasConversion(new StronglyTypedIdConverter<EspecialidadId>())
            .IsRequired();

        builder.Property<byte[]>("Version")
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}