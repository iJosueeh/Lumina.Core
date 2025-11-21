using Docentes.Domain.Abstractions;

namespace Docentes.Domain.Docentes;

public record DocenteId(Guid Value) : IStronglyTypedId;