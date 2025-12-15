using System;
using System.Threading.Tasks;

namespace Estudiantes.Domain.Estudiantes;

public interface IInscripcionRepository
{
    Task AddAsync(Inscripcion inscripcion);
}
