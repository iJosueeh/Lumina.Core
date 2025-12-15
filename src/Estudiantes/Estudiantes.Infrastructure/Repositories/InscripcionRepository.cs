using Estudiantes.Domain.Estudiantes;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Estudiantes.Infrastructure.Repositories;

public class InscripcionRepository : IInscripcionRepository
{
    private readonly ApplicationDbContext _context;

    public InscripcionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Inscripcion inscripcion)
    {
        await _context.Set<Inscripcion>().AddAsync(inscripcion);
    }
}
