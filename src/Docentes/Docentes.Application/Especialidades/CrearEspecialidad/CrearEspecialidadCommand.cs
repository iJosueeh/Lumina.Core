using Docentes.Application.Abstractions.Messaging;

namespace Docentes.Application.Especialidades.CrearEspecialidad;

public record CrearEspecialidadCommand(string Nombre, string Descripcion) : ICommand<Guid>;