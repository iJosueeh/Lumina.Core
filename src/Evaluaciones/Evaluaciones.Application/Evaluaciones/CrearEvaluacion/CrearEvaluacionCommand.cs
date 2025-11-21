using Evaluaciones.Domain.Evaluaciones;
using MediatR;

namespace Evaluaciones.Application.Evaluaciones.CrearEvaluacion;

public record CrearEvaluacionCommand(
    Guid CursoId,
    Guid DocenteId,
    string Titulo,
    string Descripcion,
    DateTime FechaInicio,
    DateTime FechaFin,
    decimal PuntajeMaximo,
    TipoEvaluacion TipoEvaluacion
) : IRequest<Guid>;