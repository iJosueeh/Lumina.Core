using System;

namespace Cursos.Domain.Cursos;

public class Testimonio
{
    public Guid Id { get; private set; }
    public string Autor { get; private set; }
    public string Comentario { get; private set; }
    public int Calificacion { get; private set; }
    public string AvatarUrl { get; private set; }

    private Testimonio(Guid id, string autor, string comentario, int calificacion, string avatarUrl)
    {
        Id = id;
        Autor = autor;
        Comentario = comentario;
        Calificacion = calificacion;
        AvatarUrl = avatarUrl;
    }

    public static Testimonio Create(string autor, string comentario, int calificacion, string avatarUrl)
    {
        if (calificacion < 1 || calificacion > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(calificacion), "La calificación debe estar entre 1 y 5.");
        }

        return new Testimonio(Guid.NewGuid(), autor, comentario, calificacion, avatarUrl);
    }
}
