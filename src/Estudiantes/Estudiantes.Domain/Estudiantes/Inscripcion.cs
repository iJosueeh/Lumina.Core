using Estudiantes.Domain.Abstractions;
using System;

namespace Estudiantes.Domain.Estudiantes;

public class Inscripcion : Entity
{
    public Guid EstudianteId { get; private set; }
    public Guid CursoId { get; private set; }
    public DateTime FechaInscripcion { get; private set; }
    public string Estado { get; private set; }

    private Inscripcion(Guid id, Guid estudianteId, Guid cursoId, DateTime fechaInscripcion, string estado)
        : base(id)
    {
        EstudianteId = estudianteId;
        CursoId = cursoId;
        FechaInscripcion = fechaInscripcion;
        Estado = estado;
    }

    public static Inscripcion Create(Guid estudianteId, Guid cursoId)
    {
        return new Inscripcion(Guid.NewGuid(), estudianteId, cursoId, DateTime.UtcNow, "Activa");
    }
}
