using System; // For Guid
using System.Collections.Generic;

namespace Cursos.Api.Controllers.Cursos;

public record CrearCursoRequest
(
    string? Nombre,
    string? Descripcion,
    int Capacidad,
    string? Nivel,
    string? Duracion,
    decimal? Precio,
    string? ImagenUrl,
    string? Categoria,
    Guid? InstructorId,
    List<ModuloRequest>? Modulos,
    List<string>? Requisitos
);

public record ModuloRequest(string Titulo, string Descripcion, List<string> Lecciones);