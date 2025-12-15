using Estudiantes.Domain.Abstractions;
using MediatR;

namespace Estudiantes.Application.Estudiantes.GetDashboardStats;

public sealed record GetDashboardStatsQuery(Guid EstudianteId) : IRequest<Result<DashboardStatsResponse>>;

public sealed record DashboardStatsResponse(
    int CursosActivos,
    int EvaluacionesPendientes,
    double PromedioGeneral,
    int HorasEstudio,
    int CursosCompletados,
    int HorasEstudioSemana
);
