using Docentes.Application.Abstractions.Messaging;
using Docentes.Domain.Especialidades;

namespace Docentes.Application.Especialidades.ObtenerEspecialidad;

public sealed record GetEspecialidadByIdQuery(EspecialidadId EspecialidadId) : IQuery<EspecialidadResponse>;