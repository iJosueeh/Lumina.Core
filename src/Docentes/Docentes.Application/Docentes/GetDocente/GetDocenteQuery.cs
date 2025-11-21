using Docentes.Application.Abstractions.Messaging;
using Docentes.Domain.Docentes;

namespace Docentes.Application.Docentes.GetDocente;

public sealed record GetDocenteQuery(DocenteId DocenteId) : IQuery<DocenteResponse>;