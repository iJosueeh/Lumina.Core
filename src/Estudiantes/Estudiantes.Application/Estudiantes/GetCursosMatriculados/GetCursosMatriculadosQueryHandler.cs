using Estudiantes.Application.Services;
using Estudiantes.Domain.Abstractions;
using Estudiantes.Domain.Matriculas;
using MediatR;

namespace Estudiantes.Application.Estudiantes.GetCursosMatriculados;

internal sealed class GetCursosMatriculadosQueryHandler : IRequestHandler<GetCursosMatriculadosQuery, Result<List<CursoMatriculadoResponse>>>
{
    private readonly IMatriculaRepository _matriculaRepository;
    private readonly ICursosService _cursosService;

    public GetCursosMatriculadosQueryHandler(IMatriculaRepository matriculaRepository, ICursosService cursosService)
    {
        _matriculaRepository = matriculaRepository;
        _cursosService = cursosService;
    }

    public async Task<Result<List<CursoMatriculadoResponse>>> Handle(GetCursosMatriculadosQuery request, CancellationToken cancellationToken)
    {
        var matriculas = await _matriculaRepository.GetByEstudianteIdAsync(request.EstudianteId, cancellationToken);
        
        var response = new List<CursoMatriculadoResponse>();

        foreach (var matricula in matriculas)
        {
            if (matricula.Programacion == null) continue;

            var cursoInfo = await _cursosService.GetCursoInfoAsync(matricula.Programacion.CursoId, cancellationToken);
            
            if (cursoInfo == null)
            {
                // Fallback si falla el servicio de cursos
                response.Add(new CursoMatriculadoResponse(
                    matricula.Programacion.CursoId.ToString(),
                    "Curso No Disponible",
                    "assets/images/courses/default.jpg",
                    0, // Progreso placeholder
                    matricula.FechaMatricula,
                    "Desconocido",
                    10,
                    0,
                    "Información no disponible temporalmente",
                    "General",
                    "Básico",
                    "assets/images/courses/default.jpg"
                ));
                continue;
            }

            // Simulacion de progreso basado en estado
            int progreso = matricula.EstadoMatricula == MatriculaEstados.COMPLETED ? 100 : 
                          matricula.EstadoMatricula == MatriculaEstados.CANCELLED ? 0 :
                          new Random(matricula.Id.GetHashCode()).Next(5, 90); // Random determinista para demo

            response.Add(new CursoMatriculadoResponse(
                cursoInfo.Id.ToString(),
                cursoInfo.Titulo,
                cursoInfo.ImagenUrl,
                progreso,
                matricula.FechaMatricula, // Usamos fecha matricula como ultima actividad por ahora
                "Instructor Docente", // TODO: Obtener de servicio Docentes
                20, // Total Lecciones Mock
                (int)(20 * (progreso / 100.0)),
                cursoInfo.Descripcion,
                cursoInfo.Categoria,
                cursoInfo.Nivel,
                cursoInfo.ImagenUrl
            ));
        }

        return Result.Success(response);
    }
}
