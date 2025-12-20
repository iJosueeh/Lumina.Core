using MediatR;
using NoticiasEventos.Application.Recursos.DTOs;
using NoticiasEventos.Application.Recursos.Queries;
using NoticiasEventos.Domain.Recursos;

namespace NoticiasEventos.Application.Recursos.Handlers;

public class GetRecursosQueryHandler : IRequestHandler<GetRecursosQuery, List<RecursoDto>>
{
    private readonly IRecursoRepository _repository;

    public GetRecursosQueryHandler(IRecursoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<RecursoDto>> Handle(GetRecursosQuery request, CancellationToken cancellationToken)
    {
        var recursos = await _repository.GetAllAsync(cancellationToken);

        var query = recursos.AsQueryable();

        // Filtros
        if (!string.IsNullOrWhiteSpace(request.Categoria) && request.Categoria.ToLower() != "todos")
        {
            query = query.Where(r => r.Categoria.ToLower() == request.Categoria.ToLower());
        }

        if (!string.IsNullOrWhiteSpace(request.Tipo))
        {
            query = query.Where(r => r.Tipo.ToLower() == request.Tipo.ToLower());
        }

        if (request.EsDestacado.HasValue)
        {
            query = query.Where(r => r.EsDestacado == request.EsDestacado.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(r => 
                r.Titulo.ToLower().Contains(search) || 
                r.Descripcion.ToLower().Contains(search));
        }

        // Ordenar por fecha descendente
        query = query.OrderByDescending(r => r.FechaPublicacion);

        // Paginación
        var pagedRecursos = query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        return pagedRecursos.Select(r => new RecursoDto(
            r.Id,
            r.Titulo,
            r.Descripcion,
            r.Url,
            r.Tipo,
            r.Categoria,
            r.Autor,
            r.FechaPublicacion,
            r.EsDestacado,
            r.ImagenUrl
        )).ToList();
    }
}
