namespace Usuarios.Domain.Usuarios;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(Usuario usuario);

    void Delete(Usuario usuario);

    void Update(Usuario usuario);

    Task<IEnumerable<Usuario>> GetAllAsync();

    Task<bool> ExisteCorreoAsync(CorreoElectronico correo, CancellationToken cancellationToken = default);

    Task<Usuario?> GetByEmailAsync(CorreoElectronico email, CancellationToken cancellationToken = default);
}