using Docentes.Application.Abstractions.Messaging;
using Docentes.Domain.Abstractions;
using Docentes.Domain.Especialidades;

namespace Docentes.Application.Especialidades.ObtenerEspecialidad;

public class GetEspecialidadByIdQueryHandler(IEspecialidadRepository especialidadRepository) : IQueryHandler<GetEspecialidadByIdQuery, EspecialidadResponse>
{
    private readonly IEspecialidadRepository _especialidadRepository = especialidadRepository;

    public async Task<Result<EspecialidadResponse>> Handle(GetEspecialidadByIdQuery request, CancellationToken cancellationToken)
    {
        var especialidad = await _especialidadRepository.GetByIdAsync(request.EspecialidadId, cancellationToken);

        if (especialidad is null)
            return Result.Failure<EspecialidadResponse>(new Error("EspecialidadNotFound", "Especialidad no encontrada."));

        var response = new EspecialidadResponse(
            especialidad.Id,
            especialidad.Nombre,
            especialidad.Descripcion
        );

        return Result.Success(response);
    }
}