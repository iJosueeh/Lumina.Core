using Estudiantes.Application.Abstractions.Messaging;
using Estudiantes.Domain.Abstractions;
using Estudiantes.Domain.Estudiantes;
using System.Threading;
using System.Threading.Tasks;

namespace Estudiantes.Application.Carritos.AgregarCurso;

internal sealed class AgregarCursoAlCarritoCommandHandler : ICommandHandler<AgregarCursoAlCarritoCommand>
{
    private readonly ICarritoRepository _carritoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AgregarCursoAlCarritoCommandHandler(ICarritoRepository carritoRepository, IUnitOfWork unitOfWork)
    {
        _carritoRepository = carritoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AgregarCursoAlCarritoCommand request, CancellationToken cancellationToken)
    {
        var carrito = await _carritoRepository.GetByIdAsync(request.EstudianteId);

        if (carrito is null)
        {
            carrito = Carrito.Create(request.EstudianteId);
            await _carritoRepository.AddAsync(carrito);
        }

        carrito.AgregarCurso(request.CursoId);

        _carritoRepository.Update(carrito);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
