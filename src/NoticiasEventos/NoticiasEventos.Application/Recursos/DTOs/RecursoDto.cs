namespace NoticiasEventos.Application.Recursos.DTOs;

public record RecursoDto(
    Guid Id,
    string Titulo,
    string Descripcion,
    string Url,
    string Tipo,
    string Categoria,
    string Autor,
    DateTime FechaPublicacion,
    bool EsDestacado,
    string ImagenUrl
);
