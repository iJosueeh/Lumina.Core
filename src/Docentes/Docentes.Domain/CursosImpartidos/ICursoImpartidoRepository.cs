namespace Docentes.Domain.CursosImpartidos;

public interface ICursoImpartidoRepository
{
    Task<CursoImpartido?> GetByIdAsync(CursoImpartidoId id, CancellationToken cancellationToken = default);

    void Add(CursoImpartido usuario);
}