using Estudiantes.Application.Services;
using System.Net.Http.Json;

namespace Estudiantes.Infrastructure.Services;

public class CursoService : ICursosService
{
    private readonly HttpClient _httpClient;

    public CursoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> CursoExistsAsync(Guid cursoId, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"cursos/{cursoId}", cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<CursoExternoResponse?> GetCursoInfoAsync(Guid cursoId, CancellationToken cancellationToken)
    {
        try 
        {
            var response = await _httpClient.GetAsync($"cursos/{cursoId}", cancellationToken);
            if (!response.IsSuccessStatusCode) return null;

            // Esperamos que el DTO de Cursos coincida con lo que necesitamos o sea mappable
            // Como Cursos devuelve un objeto complejo, podriamos necesitar System.Text.Json con opciones
            var curso = await response.Content.ReadFromJsonAsync<DtoCursoApi>(cancellationToken: cancellationToken);
            
            if (curso is null) return null;

            return new CursoExternoResponse(
                curso.Id,
                curso.Titulo,
                curso.Descripcion,
                curso.Imagen ?? "assets/images/courses/default.jpg", // Fallback image
                curso.Nivel,
                curso.Categoria
            );
        }
        catch (Exception ex)
        {
           // Log error
           Console.WriteLine($"Error obteniendo curso {cursoId}: {ex.Message}");
           return null;
        }
    }

    // Clase auxiliar para deserializar la respuesta exacta de la API de Cursos
    private class DtoCursoApi
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string? Imagen { get; set; }
        public string Nivel { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
    }
}