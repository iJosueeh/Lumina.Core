using Docentes.Domain.CursosImpartidos;
using Docentes.Domain.Docentes;
using Docentes.Infrastructure.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docentes.Infrastructure.Configurations;

internal sealed class CursosImpartidosConfigurations : IEntityTypeConfiguration<CursoImpartido>
{
    public void Configure(EntityTypeBuilder<CursoImpartido> builder)
    {
        builder.ToTable("cursos_impartidos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(new StronglyTypedIdConverter<CursoImpartidoId>());

        builder.Property(x => x.DocenteId)
               .HasConversion(new NullableStronglyTypedIdConverter<DocenteId>())
               .IsRequired();

        builder.Property(x => x.CursoId)
            .IsRequired();

        builder.HasOne(x => x.Docente)
            .WithMany()
            .HasForeignKey(x => x.DocenteId)
            .IsRequired();

        builder.Property<uint>("Version")
            .IsRowVersion();
    }
}