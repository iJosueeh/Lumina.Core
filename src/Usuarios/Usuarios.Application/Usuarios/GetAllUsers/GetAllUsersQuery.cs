using MediatR;
using Usuarios.Domain.Abstractions;

namespace Usuarios.Application.Usuarios.GetAllUsers;

public record GetAllUsersQuery() : IRequest<Result<IEnumerable<UserDto>>>;