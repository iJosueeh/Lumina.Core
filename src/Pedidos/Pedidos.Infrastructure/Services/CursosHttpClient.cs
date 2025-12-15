using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Pedidos.Application.Abstractions;
using Pedidos.Application.DTOs;

namespace Pedidos.Infrastructure.Services;

public class CursosHttpClient : ICursosHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly string _cursosApiBaseUrl;

    public CursosHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _cursosApiBaseUrl = configuration["ApiSettings:CursosApiBaseUrl"] ?? throw new InvalidOperationException("ApiSettings:CursosApiBaseUrl no está configurado.");
        _httpClient.BaseAddress = new Uri(_cursosApiBaseUrl);
    }

    public async Task<CourseDetallesDto?> GetCourseDetailsAsync(Guid cursoId)
    {
        var response = await _httpClient.GetAsync($"cursos/{cursoId}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<CourseDetallesDto>();
        }

        return null;
    }
}
