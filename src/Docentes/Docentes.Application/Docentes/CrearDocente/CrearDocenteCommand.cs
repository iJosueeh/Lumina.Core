using Docentes.Application.Abstractions.Messaging;
using Docentes.Domain.Especialidades;
using System; // For Guid

namespace Docentes.Application.Docentes.CrearDocente;

public record CrearDocenteCommand
(
   Guid UsuarioId,
    EspecialidadId EspecialidadId,
    string Nombre,
    string Cargo,
    string Bio,
    string Avatar,
    string? LinkedIn
) : ICommand<Guid>;