using Docentes.Domain.Abstractions;

namespace Docentes.Domain.Especialidades;

public record EspecialidadId(Guid Value) : IStronglyTypedId;