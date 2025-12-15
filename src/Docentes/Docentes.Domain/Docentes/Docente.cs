using Docentes.Domain.Abstractions;
using Docentes.Domain.Especialidades;
using System; // Added for Guid

namespace Docentes.Domain.Docentes;

public sealed class Docente : Entity<DocenteId>, IAggregateRoot
{
    private Docente()
    {
        EspecialidadId = null!;
        Nombre = null!; // Initialize non-nullable string
        Cargo = null!; // Initialize non-nullable string
        Bio = null!; // Initialize non-nullable string
        Avatar = null!; // Initialize non-nullable string
    }

    private Docente(
        DocenteId id,
        Guid usuarioId,
        EspecialidadId especialidadId,
        string nombre,
        string cargo,
        string bio,
        string avatar,
        string? linkedIn
    ) : base(id)
    {
        UsuarioId = usuarioId;
        EspecialidadId = especialidadId;
        Nombre = nombre;
        Cargo = cargo;
        Bio = bio;
        Avatar = avatar;
        LinkedIn = linkedIn;
    }

    public Guid UsuarioId { get; private set; }
    public EspecialidadId EspecialidadId { get; private set; }
    public string Nombre { get; private set; }
    public string Cargo { get; private set; }
    public string Bio { get; private set; }
    public string Avatar { get; private set; }
    public string? LinkedIn { get; private set; }

    public static Result<Docente> Create(
        Guid usuarioId,
        EspecialidadId especialidadId,
        string nombre,
        string cargo,
        string bio,
        string avatar,
        string? linkedIn
    )
    {
        var docente = new Docente(
            new DocenteId(Guid.NewGuid()),
            usuarioId,
            especialidadId,
            nombre,
            cargo,
            bio,
            avatar,
            linkedIn
        );

        return docente;
    }
}