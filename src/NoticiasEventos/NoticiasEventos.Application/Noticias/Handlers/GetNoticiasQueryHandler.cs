using MediatR;
using NoticiasEventos.Application.Noticias.DTOs;
using NoticiasEventos.Application.Noticias.Queries;
using NoticiasEventos.Domain.Noticias;

namespace NoticiasEventos.Application.Noticias.Handlers;

public class GetNoticiasQueryHandler : IRequestHandler<GetNoticiasQuery, List<NoticiaDto>>
{
    private readonly INoticiaRepository _repository;

    public GetNoticiasQueryHandler(INoticiaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<NoticiaDto>> Handle(GetNoticiasQuery request, CancellationToken cancellationToken)
    {
        var noticias = await _repository.GetAllAsync(cancellationToken);

        var query = noticias.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Categoria) && request.Categoria.ToLower() != "todos")
        {
            query = query.Where(n => n.Categoria.ToLower() == request.Categoria.ToLower());
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(n => 
                n.Titulo.ToLower().Contains(search) || 
                n.Descripcion.ToLower().Contains(search));
        }

        // Ordenar por fecha descendente
        query = query.OrderByDescending(n => n.Fecha);

        // Paginación
        var pagedNoticias = query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        return pagedNoticias.Select(n => new NoticiaDto(
            n.Id,
            n.Titulo,
            n.Descripcion,
            n.ImagenUrl,
            n.Fecha,
            n.Categoria,
            n.Badge,
            n.Autor,
            n.AutorAvatar,
            n.TiempoLectura,
            n.Contenido,
            n.Tags
        )).ToList();
    }
}
