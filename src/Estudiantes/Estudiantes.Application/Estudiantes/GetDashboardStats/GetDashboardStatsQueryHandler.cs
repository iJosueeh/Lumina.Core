using Estudiantes.Domain.Abstractions;
using Estudiantes.Domain.Matriculas;
using MediatR;

namespace Estudiantes.Application.Estudiantes.GetDashboardStats;

internal sealed class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, Result<DashboardStatsResponse>>
{
    private readonly IMatriculaRepository _matriculaRepository;

    public GetDashboardStatsQueryHandler(IMatriculaRepository matriculaRepository)
    {
        _matriculaRepository = matriculaRepository;
    }

    public async Task<Result<DashboardStatsResponse>> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        // 1. Obtener matrículas del estudiante
        var matriculas = await _matriculaRepository.GetByEstudianteIdAsync(request.EstudianteId, cancellationToken);
        
        // 2. Calcular estadisticas reales
        var cursosActivos = matriculas.Count(m => m.EstadoMatricula == MatriculaEstados.CONFIRMED || m.EstadoMatricula == MatriculaEstados.PENDING);
        var cursosCompletados = matriculas.Count(m => m.EstadoMatricula == MatriculaEstados.COMPLETED);
        
        // Mocks pendientes de otros microservicios
        var evaluacionesPendientes = 2; // TODO: Consultar Evaluaciones Microservice
        var promedioGeneral = 15.5; // TODO: Calcular real
        var horasEstudio = cursosCompletados * 20 + cursosActivos * 5; // Estimacion
        
        var stats = new DashboardStatsResponse(
            CursosActivos: cursosActivos,
            EvaluacionesPendientes: evaluacionesPendientes,
            PromedioGeneral: promedioGeneral,
            HorasEstudio: horasEstudio,
            CursosCompletados: cursosCompletados,
            HorasEstudioSemana: 10 // Hardcoded por ahora
        );

        return Result.Success(stats);
    }
}
