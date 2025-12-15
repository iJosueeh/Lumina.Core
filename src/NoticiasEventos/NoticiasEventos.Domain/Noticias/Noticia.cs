using NoticiasEventos.Domain.Abstractions;
using NoticiasEventos.Domain.Noticias.ValueObjects;

namespace NoticiasEventos.Domain.Noticias;

public class Noticia : Entity
{
    public string Titulo { get; private set; }
    public string Descripcion { get; private set; }
    public string ImagenUrl { get; private set; }
    public DateTime Fecha { get; private set; }
    public string Categoria { get; private set; }
    public Badge Badge { get; private set; }
    
    // Propiedades extendidas para detalle
    public string? Autor { get; private set; }
    public string? AutorAvatar { get; private set; }
    public string? TiempoLectura { get; private set; }
    public string? Contenido { get; private set; }
    public List<string> Tags { get; private set; } = new();

    // Constructor privado para EF/MongoDB
    private Noticia() {}

    public static Noticia Create(
        string titulo, 
        string descripcion, 
        string imagenUrl, 
        DateTime fecha, 
        string categoria, 
        Badge badge,
        string? autor = null,
        string? autorAvatar = null,
        string? tiempoLectura = null,
        string? contenido = null,
        List<string>? tags = null)
    {
        return new Noticia
        {
            Id = Guid.NewGuid(),
            Titulo = titulo,
            Descripcion = descripcion,
            ImagenUrl = imagenUrl,
            Fecha = fecha,
            Categoria = categoria,
            Badge = badge,
            Autor = autor,
            AutorAvatar = autorAvatar,
            TiempoLectura = tiempoLectura,
            Contenido = contenido,
            Tags = tags ?? new List<string>()
        };
    }
}
