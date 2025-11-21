using Evaluaciones.Application.Evaluaciones.Common;
using Evaluaciones.Domain.Evaluaciones;
using MediatR;

namespace Evaluaciones.Application.Evaluaciones.ObtenerEvaluacion;

public class GetEvaluacionByIdQueryHandler(IEvaluacionRepository evaluacionRepository) : IRequestHandler<GetEvaluacionByIdQuery, EvaluacionDto?>
{
    private readonly IEvaluacionRepository _evaluacionRepository = evaluacionRepository;

    public async Task<EvaluacionDto?> Handle(GetEvaluacionByIdQuery request, CancellationToken cancellationToken)
    {
        var evaluacion = await _evaluacionRepository.GetByIdAsync(new EvaluacionId(request.EvaluacionId));
        if (evaluacion == null)
        {
            return null;
        }

        return new EvaluacionDto(
            evaluacion.Id.Value,
            evaluacion.CursoId,
            evaluacion.DocenteId,
            evaluacion.Titulo,
            evaluacion.Descripcion,
            evaluacion.FechaCreacion,
            evaluacion.FechaInicio,
            evaluacion.FechaFin,
            evaluacion.PuntajeMaximo,
            evaluacion.TipoEvaluacion.ToString(),
            evaluacion.Estado.ToString()
        );
    }
}