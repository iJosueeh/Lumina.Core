using Cursos.Application.Abstractions.Messaging;
using Cursos.Domain.Abstractions;
using Cursos.Domain.Cursos;
using System.Collections.Generic;
using System.Linq;

namespace Cursos.Application.Cursos.CrearCurso;

internal sealed class CrearCursoCommandHandler : ICommandHandler<CrearCursoCommand, Guid>
{
    private readonly ICursoRepository _cursoRepository;

    public CrearCursoCommandHandler(ICursoRepository cursoRepository)
    {
        _cursoRepository = cursoRepository;
    }

    public async Task<Result<Guid>> Handle(CrearCursoCommand request, CancellationToken cancellationToken)
    {
        var curso = Curso.Create(
            Guid.NewGuid(),
            new NombreCurso(request.NombreCurso),
            new DescripcionCurso(request.DescripcionCurso),
            new CapacidadCurso(request.CapacidadCurso),
            request.Nivel,
            request.Duracion,
            request.Precio,
            request.ImagenUrl,
            request.Categoria,
            request.InstructorId,
            request.Modulos?.Select(m => new Modulo(m.Id, m.Titulo, m.Descripcion, m.Lecciones)).ToList(),
            request.Requisitos
        );

        await _cursoRepository.AddAsync(curso);
        
        return Result.Success(curso.Id);
    }
}