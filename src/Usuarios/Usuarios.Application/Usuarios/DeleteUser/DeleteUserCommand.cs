using MediatR;
using Usuarios.Domain.Abstractions;

namespace Usuarios.Application.Usuarios.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest<Result>;