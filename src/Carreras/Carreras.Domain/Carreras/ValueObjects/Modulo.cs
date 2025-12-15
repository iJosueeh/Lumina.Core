namespace Carreras.Domain.Carreras.ValueObjects;

public record Modulo(
    int Numero,
    string Nombre,
    List<CursoModulo> Cursos
);
