using Estudiantes.Domain.Abstractions;
using MediatR;

namespace Estudiantes.Application.Estudiantes.GetCursosMatriculados;

internal sealed class GetCursosMatriculadosQueryHandler : IRequestHandler<GetCursosMatriculadosQuery, Result<List<CursoMatriculadoResponse>>>
{
    public async Task<Result<List<CursoMatriculadoResponse>>> Handle(GetCursosMatriculadosQuery request, CancellationToken cancellationToken)
    {
        // MOCK DATA TEMPORAL
        var cursos = new List<CursoMatriculadoResponse>
        {
            new(
                Id: "1",
                Titulo: "Introducción a .NET 8",
                ImagenUrl: "assets/images/courses/dotnet.jpg",
                Progreso: 75,
                UltimaActividad: DateTime.UtcNow,
                Instructor: "Juan Pérez",
                TotalLecciones: 20,
                LeccionesCompletadas: 15,
                Descripcion: "Curso introductorio a .NET 8",
                Categoria: "Desarrollo Backend",
                Nivel: "Principiante",
                Imagen: "assets/images/courses/dotnet.jpg"
            ),
            new(
                Id: "2",
                Titulo: "Angular Avanzado",
                ImagenUrl: "assets/images/courses/angular.jpg",
                Progreso: 30,
                UltimaActividad: DateTime.UtcNow,
                Instructor: "Ana García",
                TotalLecciones: 25,
                LeccionesCompletadas: 8,
                Descripcion: "Curso avanzado de Angular",
                Categoria: "Desarrollo Frontend",
                Nivel: "Avanzado",
                Imagen: "assets/images/courses/angular.jpg"
            ),
            new(
                Id: "3",
                Titulo: "SQL Server para Desarrolladores",
                ImagenUrl: "assets/images/courses/sql.jpg",
                Progreso: 0,
                UltimaActividad: DateTime.UtcNow,
                Instructor: "Carlos López",
                TotalLecciones: 15,
                LeccionesCompletadas: 0,
                Descripcion: "Optimización y diseño de bases de datos",
                Categoria: "Base de Datos",
                Nivel: "Intermedio",
                Imagen: "assets/images/courses/sql.jpg"
            )
        };

        return Result.Success(cursos);
    }
}
