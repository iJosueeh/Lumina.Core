namespace Evaluaciones.Application.Evaluaciones.Common;

public record EvaluacionDto(
    Guid Id,
    Guid CursoId,
    Guid DocenteId,
    string Titulo,
    string Descripcion,
    DateTime FechaCreacion,
    DateTime FechaInicio,
    DateTime FechaFin,
    decimal PuntajeMaximo,
    string TipoEvaluacion,
    string Estado
) {}