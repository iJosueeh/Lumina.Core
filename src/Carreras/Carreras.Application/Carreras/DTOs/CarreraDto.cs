using Carreras.Domain.Carreras.ValueObjects;

namespace Carreras.Application.Carreras.DTOs;

public record CarreraDto(
    Guid Id,
    string Nombre,
    string Descripcion,
    string ImagenUrl,
    string Categoria,
    string Duracion,
    bool Activa,
    List<string> Modalidad,
    string NivelAcademico,
    int CreditosTotales,
    string Certificacion,
    List<Modulo> PlanEstudios,
    List<string> PerfilEgresado,
    List<string> Competencias,
    List<string> CampoLaboral,
    List<string> Requisitos,
    List<Testimonio> Testimonios,
    double TasaEmpleabilidad,
    string SalarioPromedio
);
