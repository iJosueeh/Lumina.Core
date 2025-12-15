using MediatR;
using NoticiasEventos.Application.Noticias.DTOs;
using NoticiasEventos.Application.Noticias.Queries;
using NoticiasEventos.Domain.Noticias;

namespace NoticiasEventos.Application.Noticias.Handlers;

public class GetNoticiaByIdQueryHandler : IRequestHandler<GetNoticiaByIdQuery, NoticiaDto?>
{
    private readonly INoticiaRepository _repository;

    public GetNoticiaByIdQueryHandler(INoticiaRepository repository)
    {
        _repository = repository;
    }

    public async Task<NoticiaDto?> Handle(GetNoticiaByIdQuery request, CancellationToken cancellationToken)
    {
        var noticia = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (noticia == null) return null;

        return new NoticiaDto(
            noticia.Id,
            noticia.Titulo,
            noticia.Descripcion,
            noticia.ImagenUrl,
            noticia.Fecha,
            noticia.Categoria,
            noticia.Badge,
            noticia.Autor,
            noticia.AutorAvatar,
            noticia.TiempoLectura,
            noticia.Contenido,
            noticia.Tags
        );
    }
}
