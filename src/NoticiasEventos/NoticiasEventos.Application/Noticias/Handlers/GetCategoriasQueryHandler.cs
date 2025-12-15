using MediatR;
using NoticiasEventos.Application.Noticias.Queries;
using NoticiasEventos.Domain.Noticias;

namespace NoticiasEventos.Application.Noticias.Handlers;

public class GetCategoriasQueryHandler : IRequestHandler<GetCategoriasQuery, List<string>>
{
    private readonly INoticiaRepository _repository;

    public GetCategoriasQueryHandler(INoticiaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<string>> Handle(GetCategoriasQuery request, CancellationToken cancellationToken)
    {
        var noticias = await _repository.GetAllAsync(cancellationToken);
        
        return noticias
            .Select(n => n.Categoria)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
    }
}
