using Cursos.Domain.Abstractions;
using Cursos.Domain.Cursos;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cursos.Application.Cursos.GetAllCourses;

internal sealed class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, Result<List<CourseListDto>>>
{
    private readonly ICursoRepository _cursoRepository;

    public GetAllCoursesQueryHandler(ICursoRepository cursoRepository)
    {
        _cursoRepository = cursoRepository;
    }

    public async Task<Result<List<CourseListDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var cursos = await _cursoRepository.GetCursos(cancellationToken);

        if (!cursos.Any())
        {
            return Result.Failure<List<CourseListDto>>(CursoErrors.NotFounds);
        }

        var courseListDtos = cursos.Select(curso => new CourseListDto(
            curso.Id,
            curso.NombreCurso?.Value ?? string.Empty,
            curso.DescripcionCurso?.Value ?? string.Empty,
            curso.Categoria ?? "Sin Categoría", // Use new property
            curso.Nivel ?? "Desconocido", // Use new property
            curso.ImagenUrl ?? "https://via.placeholder.com/150", // Use new property
            "Nuevo", // Keep placeholder for now
            "Popular", // Keep placeholder for now
            "teal", // Keep placeholder color for now
            "blue" // Keep placeholder color for now
        )).ToList();

        return Result.Success(courseListDtos);
    }
}