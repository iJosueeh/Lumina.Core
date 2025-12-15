using Estudiantes.Application.Abstractions.Messaging;
using System;

namespace Estudiantes.Application.Carritos.EliminarCurso;

public record EliminarCursoDelCarritoCommand(Guid EstudianteId, Guid CursoId) : ICommand;
