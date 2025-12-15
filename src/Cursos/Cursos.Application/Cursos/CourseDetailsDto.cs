using System;
using System.Collections.Generic;

namespace Cursos.Application.Cursos;

public record CourseDetailsDto(
    Guid Id,
    string Titulo, // Maps to Curso.NombreCurso.Value
    string Descripcion, // Maps to Curso.DescripcionCurso.Value
    string Categoria,
    string Duracion, // Placeholder
    string Modalidad, // Placeholder
    string Certificacion, // Placeholder
    string Nivel, // Placeholder
    decimal Precio, // Placeholder
    decimal? PrecioOriginal, // Placeholder
    string Imagen, // Placeholder
    InstructorDto Instructor, // New DTO for instructor
    List<ModuleDto> Modulos, // New DTO for modules
    List<string> Requisitos, // Placeholder
    List<TestimonialDetailCourseDto> Testimonios // New DTO for testimonials
);

public record InstructorDto(
    string Nombre, // Placeholder
    string Cargo, // Placeholder
    string Bio, // Placeholder
    string Avatar, // Placeholder
    string? LinkedIn // Placeholder
);



public record TestimonialDetailCourseDto( // Placeholder for now
    string Autor,
    string Comentario,
    int Calificacion,
    string AvatarUrl
);
