using MediatR;

namespace NoticiasEventos.Application.Noticias.Queries;

public record GetCategoriasQuery : IRequest<List<string>>;
