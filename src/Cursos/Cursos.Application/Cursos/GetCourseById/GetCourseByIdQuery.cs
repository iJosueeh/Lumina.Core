using MediatR;
using Cursos.Application.Cursos;
using Cursos.Domain.Abstractions;
using System;

namespace Cursos.Application.Cursos.GetCourseById;

public record GetCourseByIdQuery(Guid CourseId) : IRequest<Result<CourseDetailsDto>>;
