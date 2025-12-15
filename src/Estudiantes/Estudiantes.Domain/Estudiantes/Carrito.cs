using Estudiantes.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Estudiantes.Domain.Estudiantes;

public class Carrito : Entity
{
    public List<Guid> CursoIds { get; private set; } = new();

    private Carrito(Guid id) : base(id)
    {
    }

    public static Carrito Create(Guid estudianteId)
    {
        return new Carrito(estudianteId);
    }

    public void AgregarCurso(Guid cursoId)
    {
        if (!CursoIds.Contains(cursoId))
        {
            CursoIds.Add(cursoId);
        }
    }

    public void EliminarCurso(Guid cursoId)
    {
        CursoIds.Remove(cursoId);
    }
}
