using MediatR;
using NoticiasEventos.Application.Noticias.DTOs;

namespace NoticiasEventos.Application.Noticias.Queries;

public record GetNoticiaByIdQuery(Guid Id) : IRequest<NoticiaDto?>;
