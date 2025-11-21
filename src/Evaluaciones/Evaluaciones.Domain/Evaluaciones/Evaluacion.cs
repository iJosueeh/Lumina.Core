
namespace Evaluaciones.Domain.Evaluaciones;

using global::Evaluaciones.Domain.Abstractions;

public sealed class Evaluacion : Entity, IAggregateRoot
{
    public new EvaluacionId Id { get; private set; }
    public Guid CursoId { get; private set; }
    public Guid DocenteId { get; private set; }
    public string Titulo { get; private set; }
    public string Descripcion { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public DateTime FechaInicio { get; private set; }
    public DateTime FechaFin { get; private set; }
    public decimal PuntajeMaximo { get; private set; }
    public TipoEvaluacion TipoEvaluacion { get; private set; }
    public EstadoEvaluacion Estado { get; private set; }

    public Evaluacion(
        EvaluacionId id,
        Guid cursoId,
        Guid docenteId,
        string titulo,
        string descripcion,
        DateTime fechaCreacion,
        DateTime fechaInicio,
        DateTime fechaFin,
        decimal puntajeMaximo,
        TipoEvaluacion tipoEvaluacion,
        EstadoEvaluacion estado) : base(id.Value)
    {
        Id = id;
        CursoId = cursoId;
        DocenteId = docenteId;
        Titulo = titulo;
        Descripcion = descripcion;
        FechaCreacion = fechaCreacion;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        PuntajeMaximo = puntajeMaximo;
        TipoEvaluacion = tipoEvaluacion;
        Estado = estado;
    }

    public static Evaluacion Create(
        Guid cursoId,
        Guid docenteId,

        string titulo,
        string descripcion,
        DateTime fechaInicio,
        DateTime fechaFin,
        decimal puntajeMaximo,
        TipoEvaluacion tipoEvaluacion
    )
    {
        var evaluacion = new Evaluacion(
            new EvaluacionId(Guid.NewGuid()),
            cursoId,
            docenteId,
            titulo,
            descripcion,
            DateTime.UtcNow,
            fechaInicio,
            fechaFin,
            puntajeMaximo,
            tipoEvaluacion,
            EstadoEvaluacion.Borrador
        );
        return evaluacion;
    }

    public void PublicarEvaluacion()
    {
        if (Estado == EstadoEvaluacion.Borrador)
        {
            Estado = EstadoEvaluacion.Publicada;
        }
    }

}