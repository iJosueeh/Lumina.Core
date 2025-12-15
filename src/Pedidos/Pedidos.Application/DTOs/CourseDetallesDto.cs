namespace Pedidos.Application.DTOs;

public class CourseDetallesDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public decimal Precio { get; set; }
}