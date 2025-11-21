namespace Docentes.Application.Especialidades.Common;

public record EspecialidadDto(
    Guid Id,
    string Nombre,
    string Descripcion
);