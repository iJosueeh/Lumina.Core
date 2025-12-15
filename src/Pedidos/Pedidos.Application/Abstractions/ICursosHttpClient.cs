using Pedidos.Application.DTOs;

namespace Pedidos.Application.Abstractions;

public interface ICursosHttpClient
{
    Task<CourseDetallesDto?> GetCourseDetailsAsync(Guid cursoId);
}
