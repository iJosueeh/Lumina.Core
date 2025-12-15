using System;

namespace Cursos.Application.Cursos;

public record CourseListDto(
    Guid Id,
    string Titulo, // Maps to Curso.NombreCurso.Value
    string Descripcion, // Maps to Curso.DescripcionCurso.Value
    string Categoria, // Placeholder for now, or derived from other entity
    string Nivel, // Placeholder
    string Imagen, // Placeholder
    string Badge1, // Placeholder
    string Badge2, // Placeholder
    string ColorBadge1, // Placeholder
    string ColorBadge2 // Placeholder
);
