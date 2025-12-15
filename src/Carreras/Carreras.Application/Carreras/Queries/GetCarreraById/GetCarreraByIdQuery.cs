using Carreras.Application.Carreras.DTOs;
using Carreras.Domain.Carreras;
using Carreras.Domain.Carreras.ValueObjects;
using MediatR;

namespace Carreras.Application.Carreras.Queries.GetCarreraById;

public record GetCarreraByIdQuery(Guid Id) : IRequest<CarreraDto?>;

public class GetCarreraByIdQueryHandler : IRequestHandler<GetCarreraByIdQuery, CarreraDto?>
{
    private readonly ICarreraRepository _carreraRepository;

    public GetCarreraByIdQueryHandler(ICarreraRepository carreraRepository)
    {
        _carreraRepository = carreraRepository;
    }

    public async Task<CarreraDto?> Handle(GetCarreraByIdQuery request, CancellationToken cancellationToken)
    {
        var carrera = await _carreraRepository.GetByIdAsync(request.Id, cancellationToken);

        if (carrera is null)
        {
            return null;
        }

        return new CarreraDto(
            carrera.Id,
            carrera.Nombre,
            carrera.Descripcion,
            carrera.ImagenUrl,
            carrera.Categoria,
            carrera.Duracion,
            carrera.Activa,
            carrera.Modalidad,
            carrera.NivelAcademico,
            carrera.CreditosTotales,
            carrera.Certificacion,
            carrera.PlanEstudios,
            carrera.PerfilEgresado,
            carrera.Competencias,
            carrera.CampoLaboral,
            carrera.Requisitos,
            carrera.Testimonios,
            carrera.TasaEmpleabilidad,
            carrera.SalarioPromedio
        );
    }
}
