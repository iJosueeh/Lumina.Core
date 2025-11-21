using Usuarios.Application.Usuarios;
using Usuarios.Domain.Abstractions;

namespace Usuarios.Application.Abstractions.Services;

public interface IUserService
{
    Task<Result<IEnumerable<UserDto>>> GetAllAsync();
    Task<Result<UserDto>> GetByIdAsync(Guid id);
    Task<Result<Guid>> CreateAsync(UserManagementDto userDto); // Create user with admin privileges
    Task<Result> UpdateAsync(Guid id, UserManagementDto userDto);
    Task<Result> DeleteAsync(Guid id);
}
