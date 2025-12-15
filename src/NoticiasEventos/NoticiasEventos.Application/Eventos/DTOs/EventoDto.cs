namespace NoticiasEventos.Application.Eventos.DTOs;

public record EventoDto(
    Guid Id,
    string Titulo,
    DateTime Fecha,
    string Hora,
    string Tipo,
    string BotonTexto,
    string BotonTipo,
    string Mes,
    string Dia,
    bool EsProximo
);
