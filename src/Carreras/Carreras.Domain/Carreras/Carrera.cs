using Carreras.Domain.Abstractions;
using Carreras.Domain.Carreras.ValueObjects;

namespace Carreras.Domain.Carreras;

public class Carrera : Entity
{
    private Carrera(
        Guid id,
        string nombre,
        string description,
        string imagenUrl,
        string categoria,
        string duracion
    ) : base(id)
    {
        Nombre = nombre;
        Descripcion = description;
        ImagenUrl = imagenUrl;
        Categoria = categoria;
        Duracion = duracion;
        Activa = true;
    }

    private Carrera() {}

    public string Nombre { get; private set; }
    public string Descripcion { get; private set; }
    public string ImagenUrl { get; private set; }
    public string Categoria { get; private set; }
    public string Duracion { get; private set; }
    public bool Activa { get; private set; }

    // Rich Data Properties
    public List<string> Modalidad { get; private set; } = new();
    public string NivelAcademico { get; private set; } = string.Empty;
    public int CreditosTotales { get; private set; }
    public string Certificacion { get; private set; } = string.Empty;

    public List<Modulo> PlanEstudios { get; private set; } = new();
    public List<string> PerfilEgresado { get; private set; } = new();
    public List<string> Competencias { get; private set; } = new();
    public List<string> CampoLaboral { get; private set; } = new();
    public List<string> Requisitos { get; private set; } = new();
    public List<Testimonio> Testimonios { get; private set; } = new();

    public double TasaEmpleabilidad { get; private set; }
    public string SalarioPromedio { get; private set; } = string.Empty;

    public static Carrera Create(
        string nombre,
        string description,
        string imagenUrl,
        string categoria,
        string duracion
    )
    {
        return new Carrera(
            Guid.NewGuid(),
            nombre,
            description,
            imagenUrl,
            categoria,
            duracion
        );
    }

    public void UpdateRichData(
        List<string> modalidad,
        string nivelAcademico,
        int creditosTotales,
        string certificacion,
        List<Modulo> planEstudios,
        List<string> perfilEgresado,
        List<string> competencias,
        List<string> campoLaboral,
        List<string> requisitos,
        List<Testimonio> testimonios,
        double tasaEmpleabilidad,
        string salarioPromedio
    )
    {
        Modalidad = modalidad;
        NivelAcademico = nivelAcademico;
        CreditosTotales = creditosTotales;
        Certificacion = certificacion;
        PlanEstudios = planEstudios;
        PerfilEgresado = perfilEgresado;
        Competencias = competencias;
        CampoLaboral = campoLaboral;
        Requisitos = requisitos;
        Testimonios = testimonios;
        TasaEmpleabilidad = tasaEmpleabilidad;
        SalarioPromedio = salarioPromedio;
    }

    public void Update(string nombre, string description, string imagenUrl, string categoria, string duracion)
    {
        Nombre = nombre;
        Descripcion = description;
        ImagenUrl = imagenUrl;
        Categoria = categoria;
        Duracion = duracion;
    }

    public void Desactivar()
    {
        Activa = false;
    }
}
