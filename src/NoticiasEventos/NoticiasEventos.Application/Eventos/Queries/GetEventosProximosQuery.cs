using MediatR;
using NoticiasEventos.Application.Eventos.DTOs;
using NoticiasEventos.Domain.Eventos;

namespace NoticiasEventos.Application.Eventos.Queries;

public record GetEventosProximosQuery : IRequest<List<EventoDto>>;

public class GetEventosProximosQueryHandler : IRequestHandler<GetEventosProximosQuery, List<EventoDto>>
{
    private readonly IEventoRepository _repository;

    public GetEventosProximosQueryHandler(IEventoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<EventoDto>> Handle(GetEventosProximosQuery request, CancellationToken cancellationToken)
    {
        var eventos = await _repository.GetProximosAsync(cancellationToken);
        
        return eventos.Select(e => new EventoDto(
            e.Id,
            e.Titulo,
            e.Fecha,
            e.Hora,
            e.Tipo,
            e.BotonTexto,
            e.BotonTipo,
            e.Mes,
            e.Dia,
            e.EsProximo
        )).ToList();
    }
}
