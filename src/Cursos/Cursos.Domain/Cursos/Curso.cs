using Cursos.Domain.Abstractions;
using System;
using System.Collections.Generic;

namespace Cursos.Domain.Cursos;

public class Curso : Entity
{
    private Curso(
        Guid id,
        NombreCurso nombreCurso,
        DescripcionCurso? descripcionCurso,
        CapacidadCurso? capacidadCurso,
        string? nivel,
        string? duracion,
        decimal? precio,
        string? imagenUrl,
        string? categoria,
        Guid? instructorId
    ) : base(id)
    {
        NombreCurso = nombreCurso;
        DescripcionCurso = descripcionCurso;
        CapacidadCurso = capacidadCurso;
        Nivel = nivel;
        Duracion = duracion;
        Precio = precio;
        ImagenUrl = imagenUrl;
        Categoria = categoria;
        InstructorId = instructorId;
    }

    private Curso() {}

    public NombreCurso? NombreCurso { get; private set; }
    public DescripcionCurso? DescripcionCurso { get; private set; }
    public CapacidadCurso? CapacidadCurso { get; private set; }
    public string? Nivel { get; private set; }
    public string? Duracion { get; private set; }
    public decimal? Precio { get; private set; }
    public string? ImagenUrl { get; private set; }
    public string? Categoria { get; private set; }
    public Guid? InstructorId { get; private set; }

    public List<Modulo> Modulos { get; private set; } = new();
    public List<string> Requisitos { get; private set; } = new();
    public List<Testimonio> Testimonios { get; private set; } = new();

    public static Curso Create(
        Guid id,
        NombreCurso nombreCurso,
        DescripcionCurso? descripcionCurso,
        CapacidadCurso? capacidadCurso,
        string? nivel,
        string? duracion,
        decimal? precio,
        string? imagenUrl,
        string? categoria,
        Guid? instructorId,
        List<Modulo>? modulos = null,
        List<string>? requisitos = null,
        List<Testimonio>? testimonios = null
    )
    {
        var curso = new Curso(
            Guid.NewGuid(),
            nombreCurso,
            descripcionCurso,
            capacidadCurso,
            nivel,
            duracion,
            precio,
            imagenUrl,
            categoria,
            instructorId
        );

        if (modulos != null) curso.Modulos = modulos;
        if (requisitos != null) curso.Requisitos = requisitos;
        if (testimonios != null) curso.Testimonios = testimonios;

        return curso;
    }

   public void RestarCapacidad()
    {
        if (CapacidadCurso!.Value <= 0)
        {
            throw new InvalidOperationException("La capacidad del curso ya es cero.");
        }
        CapacidadCurso = new CapacidadCurso(CapacidadCurso.Value - 1);
    }
    public void AumentarCapacidad()
    {
        CapacidadCurso = new CapacidadCurso(CapacidadCurso!.Value + 1);
    }
    public bool TieneCuposDisponibles()
    {
        return CapacidadCurso!.Value > 0;
    }
}