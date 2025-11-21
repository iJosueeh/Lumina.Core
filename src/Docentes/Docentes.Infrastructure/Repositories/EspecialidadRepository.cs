using Docentes.Domain.Especialidades;

namespace Docentes.Infrastructure.Repositories;

internal sealed class EspecialidadRepository(ApplicationDbContext dbContext)
        : Repository<Especialidad, EspecialidadId>(dbContext), IEspecialidadRepository
{ }