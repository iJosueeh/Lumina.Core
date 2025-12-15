using Cursos.Application.Abstractions.Messaging;
using Cursos.Application.Cursos;
using System; // Added for Guid
using System.Collections.Generic;

namespace Cursos.Application.Cursos.CrearCurso;

public record CrearCursoCommand
(
    string NombreCurso,
    string DescripcionCurso,
    int CapacidadCurso,
    string? Nivel,
    string? Duracion,
    decimal? Precio,
    string? ImagenUrl,
    string? Categoria,
    Guid? InstructorId,
    List<ModuleDto>? Modulos,
    List<string>? Requisitos
) : ICommand<Guid>;