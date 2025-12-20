using MediatR;
using NoticiasEventos.Application.Recursos.DTOs;

namespace NoticiasEventos.Application.Recursos.Queries;

public record GetRecursosQuery(
    string? Search,
    string? Categoria,
    string? Tipo,
    bool? EsDestacado,
    int Page = 1,
    int PageSize = 10
) : IRequest<List<RecursoDto>>;
