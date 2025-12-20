namespace Evaluaciones.Application.Services;

public interface IEstudiantesService
{
    Task<List<Guid>> GetCursosMatriculadosIdsAsync(Guid estudianteId, CancellationToken cancellationToken);
}
