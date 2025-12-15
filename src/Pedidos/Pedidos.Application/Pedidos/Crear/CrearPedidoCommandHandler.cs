using MediatR;
using Pedidos.Domain.Abstractions;
using Pedidos.Domain.Pedidos;
using Pedidos.Application.Abstractions; // Add this using directive

namespace Pedidos.Application.Pedidos.Crear;

public class CrearPedidoCommandHandler : IRequestHandler<CrearPedidoCommand, Result<Guid>>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICursosHttpClient _cursosHttpClient; // Inject ICursosHttpClient

    public CrearPedidoCommandHandler(
        IPedidoRepository pedidoRepository,
        IUnitOfWork unitOfWork,
        ICursosHttpClient cursosHttpClient) // Add ICursosHttpClient to constructor
    {
        _pedidoRepository = pedidoRepository;
        _unitOfWork = unitOfWork;
        _cursosHttpClient = cursosHttpClient;
    }

    public async Task<Result<Guid>> Handle(CrearPedidoCommand request, CancellationToken cancellationToken)
    {
        var pedidoItemsData = new List<(Guid CursoId, string NombreCurso, decimal Precio)>();

        foreach (var itemCommand in request.Items)
        {
            var courseDetails = await _cursosHttpClient.GetCourseDetailsAsync(itemCommand.CursoId);

            if (courseDetails == null)
            {
                // Handle case where course details are not found
                return Result.Failure<Guid>(new Error("Pedido.CursoNotFound", $"Curso con ID {itemCommand.CursoId} no encontrado."));
            }

            pedidoItemsData.Add((courseDetails.Id, courseDetails.Titulo, courseDetails.Precio));
        }

        var pedido = Pedido.Crear(request.ClienteId, pedidoItemsData);

        _pedidoRepository.Add(pedido);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return pedido.Id.Value;
    }
}