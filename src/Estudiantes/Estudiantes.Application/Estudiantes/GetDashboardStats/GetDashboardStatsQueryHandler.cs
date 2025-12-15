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
        // Nota: Asumimos que existe un método para obtener por estudiante, si no, habrá que agregarlo al repositorio.
        // Por ahora usaremos una lógica simulada si el repositorio no tiene el método específico, 
        // pero idealmente deberíamos extender IMatriculaRepository.
        
        // TODO: Implementar IMatriculaRepository.GetByEstudianteIdAsync
        // var matriculas = await _matriculaRepository.GetByEstudianteIdAsync(request.EstudianteId, cancellationToken);
        
        // MOCK DATA TEMPORAL para validar el flujo completo antes de tocar repositorios
        var stats = new DashboardStatsResponse(
            CursosActivos: 3,
            EvaluacionesPendientes: 2,
            PromedioGeneral: 8.5,
            HorasEstudio: 120,
            CursosCompletados: 1,
            HorasEstudioSemana: 10
        );

        return Result.Success(stats);
    }
}
