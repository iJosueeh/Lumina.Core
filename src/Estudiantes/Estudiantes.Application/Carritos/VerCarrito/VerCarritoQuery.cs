using Estudiantes.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;

namespace Estudiantes.Application.Carritos.VerCarrito;

public record VerCarritoQuery(Guid EstudianteId) : IQuery<CarritoDto>;

public record CarritoDto(Guid EstudianteId, List<Guid> CursoIds);
