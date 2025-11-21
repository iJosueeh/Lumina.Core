using Docentes.Domain.Abstractions;

namespace Docentes.Domain.Especialidades;

public sealed class Especialidad : Entity<EspecialidadId>, IAggregateRoot
{
    private Especialidad()
    {
        Nombre = string.Empty;
        Descripcion = string.Empty;
    }

    private Especialidad(EspecialidadId id, string nombre, string descripcion)
        : base(id)
    {
        Nombre = nombre;
        Descripcion = descripcion;
    }

    public string Nombre { get; private set; } = string.Empty;
    public string Descripcion { get; private set; } = string.Empty;

    public static Especialidad Create(string nombre, string descripcion)
    {
        return new Especialidad(
            new EspecialidadId(Guid.NewGuid()),
            nombre,
            descripcion
        );
    }

    public void Update(string nombre, string descripcion)
    {
        Nombre = nombre;
        Descripcion = descripcion;
    }
}