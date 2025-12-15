using Estudiantes.Application.Abstractions.Messaging;
using Estudiantes.Domain.Abstractions;
using Estudiantes.Domain.Estudiantes;
using System.Threading;
using System.Threading.Tasks;

namespace Estudiantes.Application.Carritos.VerCarrito;

internal sealed class VerCarritoQueryHandler : IQueryHandler<VerCarritoQuery, CarritoDto>
{
    private readonly ICarritoRepository _carritoRepository;

    public VerCarritoQueryHandler(ICarritoRepository carritoRepository)
    {
        _carritoRepository = carritoRepository;
    }

    public async Task<Result<CarritoDto>> Handle(VerCarritoQuery request, CancellationToken cancellationToken)
    {
        var carrito = await _carritoRepository.GetByIdAsync(request.EstudianteId, cancellationToken);

        if (carrito is null)
        {
            return Result.Failure<CarritoDto>(new Error("Carrito.NotFound", "El carrito no fue encontrado."));
        }

        var carritoDto = new CarritoDto(carrito.Id, carrito.CursoIds);

        return carritoDto;
    }
}
