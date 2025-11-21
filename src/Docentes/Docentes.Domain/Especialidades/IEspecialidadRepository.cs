namespace Docentes.Domain.Especialidades;

public interface IEspecialidadRepository
{
    Task<Especialidad?> GetByIdAsync(EspecialidadId id, CancellationToken cancellationToken = default);
    void Add(Especialidad especialidad);
}