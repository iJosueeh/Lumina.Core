using Evaluaciones.Application.Evaluaciones.Common;
using MediatR;

namespace Evaluaciones.Application.Evaluaciones.ObtenerEvaluacion;

public record GetEvaluacionByIdQuery(Guid EvaluacionId) : IRequest<EvaluacionDto?>;