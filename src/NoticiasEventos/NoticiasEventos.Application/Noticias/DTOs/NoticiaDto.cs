using NoticiasEventos.Domain.Noticias.ValueObjects;

namespace NoticiasEventos.Application.Noticias.DTOs;

public record NoticiaDto(
    Guid Id,
    string Titulo,
    string Descripcion,
    string ImagenUrl,
    DateTime Fecha,
    string Categoria,
    Badge Badge,
    string? Autor = null,
    string? AutorAvatar = null,
    string? TiempoLectura = null,
    string? Contenido = null,
    List<string>? Tags = null
);
