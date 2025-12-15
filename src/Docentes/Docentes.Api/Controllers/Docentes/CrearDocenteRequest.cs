using System;

namespace Docentes.api.Controllers.Docentes;

public record CrearDocenteRequest 
(
    Guid UsuarioId,
    Guid EspecialidadId,
    string Nombre,
    string Cargo,
    string Bio,
    string Avatar,
    string? LinkedIn
);