namespace Estudiantes.Application.Services;

public interface ICursosService
{
    Task<bool> CursoExistsAsync(Guid cursoId, CancellationToken cancellationToken);
    Task<CursoExternoResponse?> GetCursoInfoAsync(Guid cursoId, CancellationToken cancellationToken);
}

public record CursoExternoResponse(
    Guid Id,
    string Titulo,
    string Descripcion,
    string ImagenUrl,
    string Nivel,
    string Categoria
);