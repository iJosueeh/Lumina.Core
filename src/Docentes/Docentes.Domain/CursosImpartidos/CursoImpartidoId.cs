using Docentes.Domain.Abstractions;

namespace Docentes.Domain.CursosImpartidos;

public record CursoImpartidoId(Guid Value) : IStronglyTypedId;