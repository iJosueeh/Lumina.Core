using Docentes.Domain.Abstractions;
using Docentes.Domain.Docentes;

namespace Docentes.Domain.CursosImpartidos;

public sealed class CursoImpartido : Entity<CursoImpartidoId>, IAggregateRoot
{
    private CursoImpartido() { }
    public CursoImpartido(
        CursoImpartidoId id,
        DocenteId? docenteId,
        Guid? cursoId) : base(id)
    {
        Id = id;
        DocenteId = docenteId;
        CursoId = cursoId;
    } 
    public DocenteId? DocenteId { get; set; }
    public Guid? CursoId { get; set; }
    public Docente? Docente { get; set; }
}