using MediatR;
using Cursos.Application.Cursos;
using Cursos.Domain.Abstractions;
using System.Collections.Generic;

namespace Cursos.Application.Cursos.GetAllCourses;

public record GetAllCoursesQuery() : IRequest<Result<List<CourseListDto>>>;