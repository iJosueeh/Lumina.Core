using Evaluaciones.Domain.Evaluaciones;
using Evaluaciones.Application.Services;
using MediatR;

namespace Evaluaciones.Application.Evaluaciones.GetEvaluacionesByEstudiante;

internal sealed class GetEvaluacionesByEstudianteQueryHandler : IRequestHandler<GetEvaluacionesByEstudianteQuery, List<EvaluacionResponse>>
{
    private readonly IEvaluacionRepository _evaluacionRepository;
    private readonly IEstudiantesService _estudiantesService;

    public GetEvaluacionesByEstudianteQueryHandler(IEvaluacionRepository evaluacionRepository, IEstudiantesService estudiantesService)
    {
        _evaluacionRepository = evaluacionRepository;
        _estudiantesService = estudiantesService;
    }

    public async Task<List<EvaluacionResponse>> Handle(GetEvaluacionesByEstudianteQuery request, CancellationToken cancellationToken)
    {
        // 1. Obtener IDs de cursos matriculados desde el microservicio de Estudiantes
        var cursoIds = await _estudiantesService.GetCursosMatriculadosIdsAsync(request.EstudianteId, cancellationToken);
        
        if (!cursoIds.Any())
        {
            return new List<EvaluacionResponse>();
        }

        // 2. Obtener evaluaciones para esos cursos
        var evaluaciones = await _evaluacionRepository.GetByCursoIdsAsync(cursoIds, cancellationToken);

        // 3. Mappear respuesta
        // Nota: En un escenario real, tambien deberiamos consultar si el estudiante ya envio la evaluacion
        // para calcular "Intentos" y estado real ("Completado", "Pendiente").
        // Por ahora, derivamos estado de la fecha limite.
        
        var response = evaluaciones.Select(e =>
        {
            var estado = DateTime.UtcNow > e.FechaFin ? "Vencido" : "Pendiente";
            
            return new EvaluacionResponse(
                e.Id.Value,
                e.Titulo,
                e.CursoId,
                "Curso " + e.CursoId.ToString().Substring(0, 8), // Placeholder nombre curso (necesitaria call a Cursos)
                e.FechaInicio,
                e.FechaFin,
                e.FechaFin, // FechaLimite asumiendo es FechaFin
                60, // Duracion placeholder
                60,
                estado,
                e.TipoEvaluacion.ToString(),
                0, // Intentos placeholder
                1 // Max intentos placeholder
            );
        }).OrderBy(e => e.FechaLimite).ToList();

        return response;
    }
}
