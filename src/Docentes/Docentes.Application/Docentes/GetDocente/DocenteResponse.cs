using Docentes.Domain.Docentes;
using Docentes.Domain.Especialidades;

namespace Docentes.Application.Docentes.GetDocente;

public sealed record DocenteResponse
(
    DocenteId Id,
    Guid UsuarioId,
    EspecialidadId EspecialidadId
);