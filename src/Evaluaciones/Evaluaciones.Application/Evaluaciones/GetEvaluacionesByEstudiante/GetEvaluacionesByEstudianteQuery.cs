using Evaluaciones.Domain.Abstractions;
using MediatR;

namespace Evaluaciones.Application.Evaluaciones.GetEvaluacionesByEstudiante;

public sealed record GetEvaluacionesByEstudianteQuery(Guid EstudianteId) : IRequest<List<EvaluacionResponse>>;

public sealed record EvaluacionResponse(
    Guid Id,
    string Titulo,
    Guid CursoId,
    string CursoNombre,
    DateTime FechaInicio,
    DateTime FechaFin,
    DateTime FechaLimite,
    int DuracionMinutos,
    int Duracion,
    string Estado,
    string Tipo,
    int Intentos,
    int IntentosMaximos
);
