using Docentes.Domain.Especialidades;

namespace Docentes.Application.Especialidades.ObtenerEspecialidad;

public sealed record EspecialidadResponse(
    EspecialidadId Id,
    string Nombre,
    string Descripcion
);