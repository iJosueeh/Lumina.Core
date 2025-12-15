using System;

namespace Cursos.Application.Cursos;

public record ModuleDto(
    Guid Id,
    string Titulo,
    string Descripcion,
    List<string> Lecciones
);
