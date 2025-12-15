using Cursos.Domain.Cursos;
using MediatR;

namespace Cursos.Application.Cursos.GetNiveles;

public record GetNivelesQuery : IRequest<List<string>>;

public class GetNivelesQueryHandler : IRequestHandler<GetNivelesQuery, List<string>>
{
    private readonly ICursoRepository _cursoRepository;

    public GetNivelesQueryHandler(ICursoRepository cursoRepository)
    {
        _cursoRepository = cursoRepository;
    }

    public async Task<List<string>> Handle(GetNivelesQuery request, CancellationToken cancellationToken)
    {
        var cursos = await _cursoRepository.GetCursos(cancellationToken);
        
        var niveles = cursos
            .Where(c => !string.IsNullOrWhiteSpace(c.Nivel))
            .Select(c => c.Nivel!)
            .Distinct()
            .OrderBy(n => n)
            .ToList();

        return niveles ?? new List<string>();
    }
}
