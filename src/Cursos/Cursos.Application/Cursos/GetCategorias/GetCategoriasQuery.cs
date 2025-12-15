using Cursos.Domain.Cursos;
using MediatR;

namespace Cursos.Application.Cursos.GetCategorias;

public record GetCategoriasQuery : IRequest<List<string>>;

public class GetCategoriasQueryHandler : IRequestHandler<GetCategoriasQuery, List<string>>
{
    private readonly ICursoRepository _cursoRepository;

    public GetCategoriasQueryHandler(ICursoRepository cursoRepository)
    {
        _cursoRepository = cursoRepository;
    }

    public async Task<List<string>> Handle(GetCategoriasQuery request, CancellationToken cancellationToken)
    {
        var cursos = await _cursoRepository.GetCursos(cancellationToken);
        
        var categorias = cursos
            .Where(c => !string.IsNullOrWhiteSpace(c.Categoria))
            .Select(c => c.Categoria!)
            .Distinct()
            .OrderBy(c => c)
            .ToList();

        return categorias ?? new List<string>();
    }
}
