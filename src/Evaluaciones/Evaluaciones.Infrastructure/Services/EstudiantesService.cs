using Evaluaciones.Application.Services;
using System.Net.Http.Json;

namespace Evaluaciones.Infrastructure.Services;

public class EstudiantesService : IEstudiantesService
{
    private readonly HttpClient _httpClient;

    public EstudiantesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Guid>> GetCursosMatriculadosIdsAsync(Guid estudianteId, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.GetAsync($"estudiantes/{estudianteId}/cursos-matriculados", cancellationToken);
            
            if (!response.IsSuccessStatusCode) return new List<Guid>();

            var cursos = await response.Content.ReadFromJsonAsync<List<CursoMatriculadoDto>>(cancellationToken: cancellationToken);
            
            return cursos?.Select(c => Guid.Parse(c.Id)).ToList() ?? new List<Guid>();
        }
        catch
        {
            // Log error
            return new List<Guid>();
        }
    }

    private class CursoMatriculadoDto
    {
        public string Id { get; set; } = string.Empty;
    }
}
