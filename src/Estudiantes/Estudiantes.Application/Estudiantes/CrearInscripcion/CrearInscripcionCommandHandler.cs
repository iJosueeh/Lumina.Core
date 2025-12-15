using Estudiantes.Application.Abstractions.Messaging;
using Estudiantes.Domain.Abstractions;
using Estudiantes.Domain.Estudiantes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Estudiantes.Application.Estudiantes.CrearInscripcion;

internal sealed class CrearInscripcionCommandHandler : ICommandHandler<CrearInscripcionCommand, Guid>
{
    private readonly IEstudianteRepository _estudianteRepository;
    private readonly IInscripcionRepository _inscripcionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CrearInscripcionCommandHandler(
        IEstudianteRepository estudianteRepository,
        IInscripcionRepository inscripcionRepository,
        IUnitOfWork unitOfWork)
    {
        _estudianteRepository = estudianteRepository;
        _inscripcionRepository = inscripcionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CrearInscripcionCommand request, CancellationToken cancellationToken)
    {
        var estudiante = await _estudianteRepository.GetByIdAsync(request.EstudianteId);

        if (estudiante is null)
        {
            return Result.Failure<Guid>(EstudianteErrors.NotFound);
        }

        var inscripcion = Inscripcion.Create(request.EstudianteId, request.CursoId);

        await _inscripcionRepository.AddAsync(inscripcion);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return inscripcion.Id;
    }
}
