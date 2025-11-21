using Docentes.Application.Abstractions.Messaging;
using Docentes.Domain.Abstractions;
using Docentes.Domain.Especialidades;

namespace Docentes.Application.Especialidades.CrearEspecialidad;

public class CrearEspecialidadCommandHandler : ICommandHandler<CrearEspecialidadCommand, Guid>
{
    private readonly IEspecialidadRepository _especialidadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CrearEspecialidadCommandHandler(
        IEspecialidadRepository especialidadRepository,
        IUnitOfWork unitOfWork
    )
    {
        _especialidadRepository = especialidadRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(CrearEspecialidadCommand request, CancellationToken cancellationToken)
    {
        var especialidad = Especialidad.Create(request.Nombre, request.Descripcion);

        _especialidadRepository.Add(especialidad);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(especialidad.Id.Value);
    }
}