using NoticiasEventos.Domain.Abstractions;

namespace NoticiasEventos.Domain.Eventos;

public class Evento : Entity
{
    public string Titulo { get; private set; }
    public DateTime Fecha { get; private set; }
    public string Hora { get; private set; }
    public string Tipo { get; private set; } // Online, Presencial, etc.
    public string BotonTexto { get; private set; }
    public string BotonTipo { get; private set; } // primary, secondary
    public bool EsProximo { get; private set; } // Para filtrar eventos futuros

    // Campos auxiliares para UI (mes, dia) derivados de Fecha
    public string Mes => Fecha.ToString("MMM").ToUpper();
    public string Dia => Fecha.Day.ToString("00");

    private Evento() {}

    public static Evento Create(
        string titulo,
        DateTime fecha,
        string hora,
        string tipo,
        string botonTexto,
        string botonTipo,
        bool esProximo)
    {
        return new Evento
        {
            Id = Guid.NewGuid(),
            Titulo = titulo,
            Fecha = fecha,
            Hora = hora,
            Tipo = tipo,
            BotonTexto = botonTexto,
            BotonTipo = botonTipo,
            EsProximo = esProximo
        };
    }
}
