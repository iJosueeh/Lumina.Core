using MediatR;
using NoticiasEventos.Application.Noticias.DTOs;

namespace NoticiasEventos.Application.Noticias.Queries;

public record GetNoticiasQuery(
    int Page = 1,
    int PageSize = 10,
    string? Categoria = null,
    string? Search = null
) : IRequest<List<NoticiaDto>>;

// Para simplificar, devolvemos solo la lista. En producción deberíamos devolver un PagedResult.
