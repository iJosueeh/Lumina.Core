using Estudiantes.Application.Abstractions.Messaging;
using System;

namespace Estudiantes.Application.Estudiantes.CrearInscripcion;

public record CrearInscripcionCommand(Guid EstudianteId, Guid CursoId) : ICommand<Guid>;
