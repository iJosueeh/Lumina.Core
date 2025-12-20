using NoticiasEventos.Domain.Noticias;
using NoticiasEventos.Domain.Noticias.ValueObjects;
using NoticiasEventos.Domain.Eventos;
using NoticiasEventos.Domain.Recursos;

namespace NoticiasEventos.Infrastructure;

public class DataSeeder
{
    private readonly INoticiaRepository _noticiaRepository;
    private readonly IEventoRepository _eventoRepository;
    private readonly IRecursoRepository _recursoRepository;

    public DataSeeder(INoticiaRepository noticiaRepository, IEventoRepository eventoRepository, IRecursoRepository recursoRepository)
    {
        _noticiaRepository = noticiaRepository;
        _eventoRepository = eventoRepository;
        _recursoRepository = recursoRepository;
    }

    public async Task SeedAsync()
    {
        // Seeding Noticias
        var noticias = await _noticiaRepository.GetAllAsync();
        if (!noticias.Any())
        {
            var noticiasToSeed = new List<Noticia>
            {
                Noticia.Create(
                    "Lanzamiento del nuevo Bootcamp de Inteligencia Artificial",
                    "La universidad abre inscripciones para el programa intensivo diseñado para formar a la próxima generación de expertos en IA y machine learning.",
                    "https://images.unsplash.com/photo-1531482615713-2afd69097998?ixlib=rb-4.0.3&auto=format&fit=crop&w=1470&q=80",
                    DateTime.Now.AddDays(-2),
                    "Académico",
                    new Badge("ACADÉMICO", "orange"),
                    "Dr. Juan Pérez",
                    "",
                    "5 min",
                    "Contenido completo aquí...",
                    new List<string> { "IA", "Bootcamp" }
                ),
                Noticia.Create(
                    "Estudiantes ganan Hackathon Regional de Ciberseguridad",
                    "Nuestro equipo obtuvo el primer lugar en la competencia anual de seguridad informática celebrada en la capital.",
                    "https://images.unsplash.com/photo-1550751827-4bd374c3f58b?ixlib=rb-4.0.3&auto=format&fit=crop&w=1470&q=80",
                    DateTime.Now.AddDays(-4),
                    "Tecnología",
                    new Badge("TECNOLOGÍA", "green"),
                    "Equipo de Seguridad",
                    "",
                    "3 min",
                    "Contenido...",
                    new List<string> { "Ciberseguridad", "Hackathon" }
                ),
                Noticia.Create(
                    "Resumen de la Conferencia de Tecnología 2023",
                    "Expertos de la industria compartieron sus conocimientos sobre las últimas tendencias en desarrollo web y móvil.",
                    "https://images.unsplash.com/photo-1544531586-fde5298cdd40?ixlib=rb-4.0.3&auto=format&fit=crop&w=1470&q=80",
                    DateTime.Now.AddDays(-8),
                    "Eventos",
                    new Badge("EVENTOS", "gray")
                ),
                Noticia.Create(
                    "Nuevos espacios de coworking para alumnos",
                    "Inauguramos modernas áreas de estudio colaborativo equipadas con internet de alta velocidad y monitores para trabajo grupal.",
                    "https://images.unsplash.com/photo-1522071820081-009f0129c71c?ixlib=rb-4.0.3&auto=format&fit=crop&w=1470&q=80",
                    DateTime.Now.AddDays(-15),
                    "Vida Estudiantil",
                    new Badge("COMUNIDAD", "yellow")
                ),
                Noticia.Create(
                    "Workshop de React: Creando aplicaciones modernas",
                    "Aprende las mejores prácticas para desarrollar aplicaciones web escalables utilizando React y Redux.",
                    "https://images.unsplash.com/photo-1633356122544-f134324a6cee?ixlib=rb-4.0.3&auto=format&fit=crop&w=1470&q=80",
                    DateTime.Now.AddDays(-20),
                    "Workshops",
                    new Badge("WORKSHOPS", "blue")
                ),
                Noticia.Create(
                    "Semana Cultural: Arte y Tecnología",
                    "Una semana llena de exposiciones que fusionan el arte digital con las últimas tecnologías inmersivas.",
                    "https://images.unsplash.com/photo-1514525253440-b393452e8d26?ixlib=rb-4.0.3&auto=format&fit=crop&w=1470&q=80",
                    DateTime.Now.AddDays(-25),
                    "Cultural",
                    new Badge("CULTURAL", "purple")
                ),
                Noticia.Create(
                    "Nuevo laboratorio de IoT inaugurado",
                    "Equipado con sensores de última generación para proyectos de Internet de las Cosas.",
                    "https://images.unsplash.com/photo-1518770660439-4636190af475?ixlib=rb-4.0.3&auto=format&fit=crop&w=1470&q=80",
                    DateTime.Now.AddDays(-30),
                    "Tecnología",
                    new Badge("TECNOLOGÍA", "green")
                ),
                Noticia.Create(
                    "Becas para mujeres en tecnología",
                    "Lanzamos un nuevo programa de becas para fomentar la participación femenina en carreras STEM.",
                    "https://images.unsplash.com/photo-1573496359142-b8d87734a5a2?ixlib=rb-4.0.3&auto=format&fit=crop&w=1470&q=80",
                    DateTime.Now.AddDays(-35),
                    "Académico",
                    new Badge("ACADÉMICO", "orange")
                )
            };

            foreach (var noticia in noticiasToSeed)
            {
                await _noticiaRepository.AddAsync(noticia);
            }
        }

        // Seeding Eventos
        var eventos = await _eventoRepository.GetProximosAsync();
        if (!eventos.Any())
        {
            var eventosToSeed = new List<Evento>
            {
                Evento.Create(
                    "Webinar: El futuro de React",
                    DateTime.Now.AddDays(10), // Fecha futura
                    "18:00 hrs",
                    "Online",
                    "REGISTRARSE",
                    "primary",
                    true
                ),
                Evento.Create(
                    "Workshop: UX/UI Design Essentials",
                    DateTime.Now.AddDays(20),
                    "10:00 hrs",
                    "Lun-Jue",
                    "VER DETALLES",
                    "secondary",
                    true
                ),
                Evento.Create(
                    "Feria de Empleabilidad Tech 2023",
                    DateTime.Now.AddDays(60),
                    "09:00 hrs",
                    "Auditorio Central",
                    "INSCRIBIRME",
                    "primary",
                    true
                )
            };

            foreach (var evento in eventosToSeed)
            {
                await _eventoRepository.AddAsync(evento);
            }
        }

        // Seeding Recursos
        var recursos = await _recursoRepository.GetAllAsync();
        if (!recursos.Any())
        {
            var recursosToSeed = new List<Recurso>
            {
                Recurso.Create(
                    "Guía Completa de Angular 17",
                    "Manual oficial actualizado con las últimas características de Angular, incluyendo Signals y Standalone Components.",
                    "https://angular.io",
                    "PDF",
                    "Guías",
                    "Google Developer Team",
                    DateTime.Now.AddDays(-5),
                    true,
                    "assets/images/resources/angular-guide.jpg"
                ),
                Recurso.Create(
                    "Introducción a Clean Architecture en .NET",
                    "Video tutorial explicativo sobre cómo estructurar microservicios utilizando Clean Architecture.",
                    "https://youtube.com/watch?v=xyz123",
                    "VIDEO",
                    "Tutoriales",
                    "Microsoft MVP",
                    DateTime.Now.AddDays(-10),
                    true,
                    "assets/images/resources/clean-arch.jpg"
                ),
                Recurso.Create(
                    "Patrones de Diseño: Singleton y Factory",
                    "Artículo detallado sobre cuándo y cómo utilizar estos patrones creacionales en C#.",
                    "https://refactoring.guru",
                    "LINK",
                    "Artículos",
                    "Refactoring Guru",
                    DateTime.Now.AddDays(-15),
                    false,
                    "assets/images/resources/patterns.jpg"
                ),
                Recurso.Create(
                    "Documentación de MongoDb Driver para C#",
                    "Referencia completa de la API del driver oficial de MongoDB para .NET.",
                    "https://mongodb.com/docs/drivers/csharp",
                    "LINK",
                    "Documentación",
                    "MongoDB Inc.",
                    DateTime.Now.AddDays(-20),
                    false,
                    "assets/images/resources/mongo-docs.jpg"
                ),
                Recurso.Create(
                    "Curso Avanzado de Docker y Kubernetes",
                    "Serie de videos para dominar la orquestación de contenedores en entornos de producción.",
                    "https://udemy.com",
                    "VIDEO",
                    "Cursos",
                    "Docker Captain",
                    DateTime.Now.AddDays(-2),
                    true,
                    "assets/images/resources/docker-course.jpg"
                )
            };

            foreach (var recurso in recursosToSeed)
            {
                _recursoRepository.Add(recurso);
            }
        }
    }
}

