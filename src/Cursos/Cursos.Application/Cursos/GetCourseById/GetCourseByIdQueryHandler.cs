using Cursos.Domain.Abstractions;
using Cursos.Domain.Cursos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cursos.Application.Cursos.GetCourseById;

internal sealed class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, Result<CourseDetailsDto>>
{
    private readonly ICursoRepository _cursoRepository;

    public GetCourseByIdQueryHandler(ICursoRepository cursoRepository)
    {
        _cursoRepository = cursoRepository;
    }

    public async Task<Result<CourseDetailsDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var curso = await _cursoRepository.GetByIdAsync(request.CourseId, cancellationToken);

        if (curso == null)
        {
            return Result.Failure<CourseDetailsDto>(CursoErrors.NotFound);
        }

        var courseDetailsDto = new CourseDetailsDto(
            curso.Id,
            curso.NombreCurso?.Value ?? string.Empty,
            curso.DescripcionCurso?.Value ?? string.Empty,
            curso.Categoria ?? "Sin Categoría",
            curso.Duracion ?? "Desconocida", // Use new property
            "Online", // Keep placeholder for now
            "Certificado Incluido", // Keep placeholder for now
            curso.Nivel ?? "Desconocido", // Use new property
            curso.Precio ?? 0m, // Use new property
            null, // Placeholder for PrecioOriginal
            curso.ImagenUrl ?? "https://via.placeholder.com/300x200", // Use new property
            new InstructorDto(
                "Juan Pérez", // Placeholder
                "Desarrollador Senior", // Placeholder
                "Experto en desarrollo web con más de 10 años de experiencia.", // Placeholder
                "https://i.pravatar.cc/150?img=1", // Placeholder
                "https://linkedin.com/in/juanperez" // Placeholder
            ),
            curso.Modulos.Select(m => new ModuleDto(m.Id, m.Titulo, m.Descripcion, m.Lecciones)).ToList(),
            curso.Requisitos,
            curso.Testimonios.Select(t => new TestimonialDetailCourseDto(t.Autor, t.Comentario, t.Calificacion, t.AvatarUrl)).ToList()
        );

        return Result.Success(courseDetailsDto);
    }
}
