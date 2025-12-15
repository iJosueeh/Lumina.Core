using Estudiantes.Application.Abstractions.Messaging;
using Estudiantes.Domain.Abstractions;
using Estudiantes.Domain.Estudiantes;
using System.Threading;
using System.Threading.Tasks;

namespace Estudiantes.Application.Carritos.EliminarCurso;

internal sealed class EliminarCursoDelCarritoCommandHandler : ICommandHandler<EliminarCursoDelCarritoCommand>
{
    private readonly ICarritoRepository _carritoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EliminarCursoDelCarritoCommandHandler(ICarritoRepository carritoRepository, IUnitOfWork unitOfWork)
    {
        _carritoRepository = carritoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(EliminarCursoDelCarritoCommand request, CancellationToken cancellationToken)
    {
        var carrito = await _carritoRepository.GetByIdAsync(request.EstudianteId);

        if (carrito is null)
        {
            return Result.Failure(new Error("Carrito.NotFound", "El carrito no fue encontrado."));
        }

        carrito.EliminarCurso(request.CursoId);

        _carritoRepository.Update(carrito);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
