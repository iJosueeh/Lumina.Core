using MediatR;
using Estudiantes.Domain.Abstractions;

namespace Estudiantes.Application.Programaciones.GetProgramacion;

public sealed record GetProgramacionPorEstudianteQuery(Guid EstudianteId) : IRequest<Result<List<ProgramacionCalendarioResponse>>>;

public sealed record ProgramacionCalendarioResponse(
    Guid Id,
    string Titulo,
    string Descripcion,
    DateTime FechaInicio,
    DateTime FechaFin,
    Guid CursoId,
    string CursoNombre,
    string Tipo,
    string EnlaceReunion,
    string DocenteId,
    string DocenteNombre,
    string Modalidad,
    int DiaSemana
);
