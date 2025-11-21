using Docentes.Domain.Abstractions;
using Docentes.Domain.Especialidades;

namespace Docentes.Domain.Docentes;

public sealed class Docente : Entity<DocenteId>, IAggregateRoot
{
    private Docente()
    {
        EspecialidadId = null!;
    }

    private Docente(
        DocenteId id,
        Guid usuarioId,
        EspecialidadId especialidadId
    ) : base(id)
    {
        UsuarioId = usuarioId;
        EspecialidadId = especialidadId;
    }

    public Guid UsuarioId { get; private set; }
    public EspecialidadId EspecialidadId { get; private set; }

    public static Result<Docente> Create(
        Guid usuarioId,
        EspecialidadId especialidadId
    )
    {
        var docente = new Docente(
            new DocenteId(Guid.NewGuid()),
            usuarioId,
            especialidadId
        );

        return docente;
    }
}