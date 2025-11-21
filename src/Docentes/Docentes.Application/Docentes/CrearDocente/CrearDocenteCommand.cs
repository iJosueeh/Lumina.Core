using Docentes.Application.Abstractions.Messaging;
using Docentes.Domain.Especialidades;

namespace Docentes.Application.Docentes.CrearDocente;

public record CrearDocenteCommand
(
   Guid UsuarioId,
    EspecialidadId EspecialidadId
) : ICommand<Guid>;