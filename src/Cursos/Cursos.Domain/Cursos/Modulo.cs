using System;

namespace Cursos.Domain.Cursos;

public class Modulo
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; }
    public string Descripcion { get; private set; }
    public List<string> Lecciones { get; private set; }

    public Modulo(Guid id, string titulo, string descripcion, List<string> lecciones)
    {
        Id = id;
        Titulo = titulo;
        Descripcion = descripcion;
        Lecciones = lecciones;
    }
}
