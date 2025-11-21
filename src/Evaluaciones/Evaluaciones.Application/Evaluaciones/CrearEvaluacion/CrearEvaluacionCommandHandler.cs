using Evaluaciones.Domain.Evaluaciones;
using MediatR;

namespace Evaluaciones.Application.Evaluaciones.CrearEvaluacion;

public class CrearEvaluacionCommandHandler(IEvaluacionRepository evaluacionRepository) : IRequestHandler<CrearEvaluacionCommand, Guid>
{
    private readonly IEvaluacionRepository _evaluacionRepository = evaluacionRepository;

    public async Task<Guid> Handle(CrearEvaluacionCommand request, CancellationToken cancellationToken)
    {
        var evaluacion = Evaluacion.Create(
            request.CursoId,
            request.DocenteId,
            request.Titulo,
            request.Descripcion,
            request.FechaInicio,
            request.FechaFin,
            request.PuntajeMaximo,
            request.TipoEvaluacion
        );

        _evaluacionRepository.Add(evaluacion);

        return evaluacion.Id.Value;
    }
}