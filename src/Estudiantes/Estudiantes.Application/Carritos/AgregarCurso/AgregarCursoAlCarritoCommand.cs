using Estudiantes.Application.Abstractions.Messaging;
using System;

namespace Estudiantes.Application.Carritos.AgregarCurso;

public record AgregarCursoAlCarritoCommand(Guid EstudianteId, Guid CursoId) : ICommand;
