using Estudiantes.Domain.Abstractions;
using MediatR;

namespace Estudiantes.Application.Estudiantes.GetCursosMatriculados;

public sealed record GetCursosMatriculadosQuery(Guid EstudianteId) : IRequest<Result<List<CursoMatriculadoResponse>>>;

public sealed record CursoMatriculadoResponse(
    string Id,
    string Titulo,
    string ImagenUrl,
    int Progreso,
    DateTime UltimaActividad,
    string Instructor,
    int TotalLecciones,
    int LeccionesCompletadas,
    string Descripcion,
    string Categoria,
    string Nivel,
    string Imagen
);
