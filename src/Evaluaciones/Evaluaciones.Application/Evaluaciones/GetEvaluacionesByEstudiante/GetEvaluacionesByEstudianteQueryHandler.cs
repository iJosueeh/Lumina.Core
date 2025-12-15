using MediatR;

namespace Evaluaciones.Application.Evaluaciones.GetEvaluacionesByEstudiante;

internal sealed class GetEvaluacionesByEstudianteQueryHandler : IRequestHandler<GetEvaluacionesByEstudianteQuery, List<EvaluacionResponse>>
{
    public Task<List<EvaluacionResponse>> Handle(GetEvaluacionesByEstudianteQuery request, CancellationToken cancellationToken)
    {
        // MOCK DATA TEMPORAL
        var evaluaciones = new List<EvaluacionResponse>
        {
            new(
                Id: Guid.NewGuid(),
                Titulo: "Examen Parcial de .NET",
                CursoId: Guid.NewGuid(),
                CursoNombre: "Desarrollo Web con .NET",
                FechaInicio: DateTime.UtcNow.AddDays(1),
                FechaFin: DateTime.UtcNow.AddDays(3),
                FechaLimite: DateTime.UtcNow.AddDays(3),
                DuracionMinutos: 60,
                Duracion: 60,
                Estado: "Pendiente",
                Tipo: "Examen",
                Intentos: 0,
                IntentosMaximos: 1
            ),
            new(
                Id: Guid.NewGuid(),
                Titulo: "Quiz de Componentes",
                CursoId: Guid.NewGuid(),
                CursoNombre: "Frontend con Angular",
                FechaInicio: DateTime.UtcNow.AddDays(2),
                FechaFin: DateTime.UtcNow.AddDays(4),
                FechaLimite: DateTime.UtcNow.AddDays(4),
                DuracionMinutos: 30,
                Duracion: 30,
                Estado: "Pendiente",
                Tipo: "Quiz",
                Intentos: 0,
                IntentosMaximos: 3
            )
        };

        return Task.FromResult(evaluaciones);
    }
}
