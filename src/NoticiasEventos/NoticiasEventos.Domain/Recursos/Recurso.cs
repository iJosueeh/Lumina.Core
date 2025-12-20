using NoticiasEventos.Domain.Abstractions;

namespace NoticiasEventos.Domain.Recursos;

public class Recurso : Entity
{
    public string Titulo { get; private set; }
    public string Descripcion { get; private set; }
    public string Url { get; private set; }
    public string Tipo { get; private set; } // PDF, VIDEO, LINK
    public string Categoria { get; private set; } // GUIA, TUTORIAL, DOCUMENTO
    public string Autor { get; private set; }
    public DateTime FechaPublicacion { get; private set; }
    public bool EsDestacado { get; private set; }
    public string ImagenUrl { get; private set; } // Thumbnail opcional

    private Recurso() { }

    public static Recurso Create(
        string titulo,
        string descripcion,
        string url,
        string tipo,
        string categoria,
        string autor,
        DateTime? fechaPublicacion,
        bool esDestacado,
        string imagenUrl)
    {
        return new Recurso
        {
            Id = Guid.NewGuid(),
            Titulo = titulo,
            Descripcion = descripcion,
            Url = url,
            Tipo = tipo,
            Categoria = categoria,
            Autor = autor,
            FechaPublicacion = fechaPublicacion ?? DateTime.UtcNow,
            EsDestacado = esDestacado,
            ImagenUrl = imagenUrl
        };
    }
}
