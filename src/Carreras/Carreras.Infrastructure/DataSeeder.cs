using Carreras.Domain.Carreras;
using Carreras.Domain.Carreras.ValueObjects;
using MongoDB.Driver;

namespace Carreras.Infrastructure;

public static class DataSeeder
{
    public static void Seed(IMongoDatabase database)
    {
        var carrerasCollection = database.GetCollection<Carrera>("carreras");

        // Force cleanup to ensure new schema is applied
        if (carrerasCollection.CountDocuments(_ => true) > 0)
        {
            carrerasCollection.DeleteMany(_ => true);
        }

        var carreras = new List<Carrera>();

        // 1. Ingeniería de Software
        var ingSoftware = Carrera.Create(
            "Ingeniería de Software",
            "Construye soluciones de software escalables y robustas que impulsan el futuro.",
            "https://images.unsplash.com/photo-1542831371-29b0f74f9713?ixlib=rb-1.2.1&auto=format&fit=crop&w=1950&q=80",
            "Desarrollo de Software",
            "4 años"
        );
        ingSoftware.UpdateRichData(
            new List<string> { "Presencial", "Online" },
            "Pregrado",
            200,
            "Ingeniero de Software",
            new List<Modulo> {
                new(1, "Semestre 1", new List<CursoModulo> {
                    new("Introducción a la Programación", 4, "Fundamentos con Python"),
                    new("Matemáticas I", 4, "Cálculo diferencial"),
                    new("Lógica Computacional", 3, "Algoritmos básicos")
                }),
                new(2, "Semestre 2", new List<CursoModulo> {
                    new("Programación Orientada a Objetos", 4, "Java y patrones"),
                    new("Estructuras de Datos", 4, "Listas, árboles, grafos"),
                    new("Base de Datos I", 3, "SQL y modelado")
                })
            },
            new List<string> { "Diseñar arquitecturas escalables", "Liderar equipos ágiles", "Implementar CI/CD" },
            new List<string> { "C#", "Java", "Python", "Docker", "Kubernetes", "React" },
            new List<string> { "Desarrollador Full-Stack", "Arquitecto de Software", "Tech Lead" },
            new List<string> { "Secundaria completa", "Examen de admisión", "Entrevista" },
            new List<Testimonio> {
                new("Ana García", "Senior Dev", "Google", "La carrera me dio las bases para triunfar en Silicon Valley.", "https://randomuser.me/api/portraits/women/44.jpg")
            },
            98.5,
            "$3,500 - $6,000 USD"
        );
        carreras.Add(ingSoftware);

        // 2. Ciencia de Datos
        var dataScience = Carrera.Create(
            "Ciencia de Datos",
            "Domina el arte de extraer conocimientos prácticos de conjuntos de datos complejos.",
            "https://images.unsplash.com/photo-1551288049-bebda4e38f71?ixlib=rb-1.2.1&auto=format&fit=crop&w=1950&q=80",
            "Ciencia de Datos",
            "4 años"
        );
        dataScience.UpdateRichData(
            new List<string> { "Presencial", "Híbrido" },
            "Pregrado",
            190,
            "Científico de Datos",
            new List<Modulo> {
                new(1, "Fundamentos", new List<CursoModulo> {
                    new("Estadística I", 4, "Probabilidad y descriptiva"),
                    new("Python para Data Science", 4, "Pandas, NumPy"),
                    new("Matemáticas Discretas", 3, "Teoría de conjuntos")
                }),
                new(2, "Machine Learning", new List<CursoModulo> {
                    new("Regresión y Clasificación", 4, "Modelos supervisados"),
                    new("Deep Learning Intro", 4, "Redes neuronales básicas"),
                    new("Visualización de Datos", 3, "Tableau y PowerBI")
                })
            },
            new List<string> { "Crear modelos predictivos", "Analizar grandes volúmenes de datos", "Comunicar insights de negocio" },
            new List<string> { "Python", "R", "SQL", "TensorFlow", "PyTorch" },
            new List<string> { "Data Scientist", "Data Analyst", "ML Engineer" },
            new List<string> { "Secundaria completa", "Conocimientos de estadística básica" },
            new List<Testimonio> {
                new("Carlos Ruiz", "Lead Data Scientist", "Amazon", "Aprendí a convertir datos en decisiones estratégicas.", "https://randomuser.me/api/portraits/men/46.jpg")
            },
            96.0,
            "$4,000 - $7,000 USD"
        );
        carreras.Add(dataScience);

        // 3. Ciberseguridad
        var ciberseguridad = Carrera.Create(
            "Ciberseguridad",
            "Protege sistemas y redes críticas de amenazas digitales en evolución.",
            "https://images.unsplash.com/photo-1550751817-4e30b9d7f379?ixlib=rb-1.2.1&auto=format&fit=crop&w=1950&q=80",
            "Ciberseguridad",
            "3.5 años"
        );
        ciberseguridad.UpdateRichData(
            new List<string> { "Online" },
            "Técnico Superior",
            160,
            "Especialista en Ciberseguridad",
            new List<Modulo> {
                new(1, "Redes y Sistemas", new List<CursoModulo> {
                    new("Fundamentos de Redes", 4, "TCP/IP, OSI"),
                    new("Sistemas Operativos", 4, "Linux/Windows Internals"),
                    new("Introducción a la Ciberseguridad", 3, "Conceptos clave y amenazas")
                })
            },
            new List<string> { "Auditoría de seguridad", "Hacking ético", "Respuesta a incidentes" },
            new List<string> { "Kali Linux", "Wireshark", "Metasploit", "Python scripting" },
            new List<string> { "Pentester", "Security Analyst", "SOC Analyst" },
            new List<string> { "Secundaria completa", "Pasión por la seguridad" },
            new List<Testimonio> {
                new("Maria Gomez", "Security Consultant", "IBM", "La metodología práctica fue clave para mi carrera.", "https://randomuser.me/api/portraits/women/32.jpg")
            },
            99.0,
            "$3,800 - $6,500 USD"
        );
        carreras.Add(ciberseguridad);

        // 4. Computación en la Nube
        var cloud = Carrera.Create(
            "Computación en la Nube",
            "Diseña y gestiona infraestructuras en la nube de próxima generación.",
            "https://images.unsplash.com/photo-1451187580459-43490279c0fa?ixlib=rb-1.2.1&auto=format&fit=crop&w=1950&q=80",
            "Computación en la Nube",
            "3 años"
        );
        cloud.UpdateRichData(
            new List<string> { "Presencial", "Online" },
            "Técnico Superior",
            150,
            "Arquitecto Cloud",
            new List<Modulo> {
                new(1, "Fundamentos Cloud", new List<CursoModulo> {
                    new("AWS Essentials", 4, "EC2, S3, RDS"),
                    new("Azure Fundamentals", 4, "Azure AD, VMs"),
                    new("Redes para Cloud", 3, "VPC, Subnets")
                })
            },
            new List<string> { "Migración a la nube", "Gestión de costos cloud", "Arquitectura serverless" },
            new List<string> { "AWS", "Azure", "Terraform", "Docker" },
            new List<string> { "Cloud Engineer", "DevOps", "SysAdmin" },
            new List<string> { "Secundaria completa" },
            new List<Testimonio> {
                new("Pedro Alva", "Cloud Architect", "Microsoft", "Certificaciones integradas en la carrera.", "https://randomuser.me/api/portraits/men/22.jpg")
            },
            97.5,
            "$4,200 - $7,000 USD"
        );
        carreras.Add(cloud);

        // 5. IA
        var ai = Carrera.Create(
            "IA y Aprendizaje Automático",
            "Construye sistemas inteligentes que aprenden y se adaptan para resolver problemas complejos.",
            "https://images.unsplash.com/photo-1555949963-ff9fe0c870eb?ixlib=rb-1.2.1&auto=format&fit=crop&w=1950&q=80",
            "IA y Aprendizaje Automático",
            "4 años"
        );
        ai.UpdateRichData(
            new List<string> { "Presencial" },
            "Pregrado",
            200,
            "Ingeniero en IA",
            new List<Modulo> {
                new(1, "Matemáticas para IA", new List<CursoModulo> {
                    new("Álgebra Lineal", 4, "Matrices y vectores"),
                    new("Cálculo Multivariable", 4, "Gradientes, optimización"),
                    new("Probabilidad Avanzada", 3, "Bayes, distribuciones")
                })
            },
            new List<string> { "Desarrollar agentes inteligentes", "Visión artificial", "Procesamiento de lenguaje natural" },
            new List<string> { "Python", "PyTorch", "OpenCV", "Hugging Face" },
            new List<string> { "AI Research Scientist", "ML Engineer", "NLP Engineer" },
            new List<string> { "Alto rendimiento en matemáticas", "Examen avanzado" },
            new List<Testimonio> {
                new("Sofia Lee", "AI Researcher", "OpenAI", "El nivel de profundidad teórica es excelente.", "https://randomuser.me/api/portraits/women/65.jpg")
            },
            98.0,
            "$5,000 - $9,000 USD"
        );
        carreras.Add(ai);

        carrerasCollection.InsertMany(carreras);
    }
}
