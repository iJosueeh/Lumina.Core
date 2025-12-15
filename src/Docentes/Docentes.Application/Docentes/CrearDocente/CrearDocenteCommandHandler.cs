using Docentes.Application.Abstractions.Messaging;
using Docentes.Application.Services;
using Docentes.Domain.Abstractions;
using Docentes.Domain.Docentes;
using Docentes.Domain.Especialidades;

namespace Docentes.Application.Docentes.CrearDocente;

internal sealed class CrearDocenteCommandHandler :
ICommandHandler<CrearDocenteCommand, Guid>
{
    private readonly IDocenteRepository _docenteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsuarioService _usuarioService;
    private readonly IEspecialidadRepository _especialidadRepository;

    public CrearDocenteCommandHandler(IDocenteRepository docenteRepository, IUnitOfWork unitOfWork, IUsuarioService usuarioService, IEspecialidadRepository especialidadRepository)
    {
        _docenteRepository = docenteRepository;
        _unitOfWork = unitOfWork;
        _usuarioService = usuarioService;
        _especialidadRepository = especialidadRepository;
    }

    public async Task<Result<Guid>> Handle(
        CrearDocenteCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _usuarioService.UsuarioExistAsync(request.UsuarioId, cancellationToken))
        {
            return Result.Failure<Guid>(new Error("UsuarioNotFound", "El usuarioId no es válido."));
        }

        var especialidad = await _especialidadRepository.GetByIdAsync(request.EspecialidadId, cancellationToken);

        if (especialidad is null)
        {
            return Result.Failure<Guid>(new Error("EspecialidadNotFound", "El especialidadId no es válido."));
        }

        var docenteResult = Docente.Create(
            request.UsuarioId,
            request.EspecialidadId,
            request.Nombre,
            request.Cargo,
            request.Bio,
            request.Avatar,
            request.LinkedIn
        );

        if (docenteResult.IsFailure)
        {
            return Result.Failure<Guid>(docenteResult.Error);
        }

        var docente = docenteResult.Value;

        _docenteRepository.Add(docente);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(docente.Id.Value);
    }
}