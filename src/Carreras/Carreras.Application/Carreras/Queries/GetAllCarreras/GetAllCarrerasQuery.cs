using Carreras.Application.Carreras.DTOs;
using Carreras.Domain.Carreras;
using Carreras.Domain.Carreras.ValueObjects;
using MediatR;

namespace Carreras.Application.Carreras.Queries.GetAllCarreras;

public record GetAllCarrerasQuery : IRequest<List<CarreraDto>>;

public class GetAllCarrerasQueryHandler : IRequestHandler<GetAllCarrerasQuery, List<CarreraDto>>
{
    private readonly ICarreraRepository _carreraRepository;

    public GetAllCarrerasQueryHandler(ICarreraRepository carreraRepository)
    {
        _carreraRepository = carreraRepository;
    }

    public async Task<List<CarreraDto>> Handle(GetAllCarrerasQuery request, CancellationToken cancellationToken)
    {
        var carreras = await _carreraRepository.GetAllAsync(cancellationToken);

        return carreras.Select(c => new CarreraDto(
            c.Id,
            c.Nombre,
            c.Descripcion,
            c.ImagenUrl,
            c.Categoria,
            c.Duracion,
            c.Activa,
            c.Modalidad,
            c.NivelAcademico,
            c.CreditosTotales,
            c.Certificacion,
            c.PlanEstudios,
            c.PerfilEgresado,
            c.Competencias,
            c.CampoLaboral,
            c.Requisitos,
            c.Testimonios,
            c.TasaEmpleabilidad,
            c.SalarioPromedio
        )).ToList();
    }
}
