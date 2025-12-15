using Cursos.Domain.Cursos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cursos.Infrastructure;

public static class DataSeeder
{
    public static void Seed(IMongoDatabase database)
    {
        var cursosCollection = database.GetCollection<Curso>("cursos");
        var instructorId1 = Guid.NewGuid();
        var instructorId2 = Guid.NewGuid();

        var cursos = new List<Curso>
        {
            Curso.Create(
                Guid.Parse("7263eddf-aad7-40ef-b5e0-75681ad945fd"),
                new NombreCurso("Curso de C# de Cero a Experto"),
                new DescripcionCurso("Aprende C# desde los fundamentos hasta temas avanzados."),
                new CapacidadCurso(50),
                "Principiante",
                "40 horas",
                199.99m,
                "https://example.com/csharp.jpg",
                "Programación",
                instructorId1,
                new List<Modulo>
                {
                    new Modulo(Guid.NewGuid(), "Introducción a C#", "Conceptos básicos del lenguaje.", new List<string> { "Variables y Tipos de Datos", "Operadores" }),
                    new Modulo(Guid.NewGuid(), "Programación Orientada a Objetos", "Clases, objetos y herencia.", new List<string> { "Clases y Objetos", "Herencia y Polimorfismo" })
                },
                new List<string> { "Conocimientos básicos de programación." },
                new List<Testimonio> { Testimonio.Create("Juan Perez", "Excelente curso!", 5, "https://i.pravatar.cc/150?img=1") }
            ),
            Curso.Create(
                Guid.Parse("a4f5f5f5-f5f5-f5f5-f5f5-f5f5f5f5f5f5"),
                new NombreCurso("Curso de ASP.NET Core MVC"),
                new DescripcionCurso("Crea aplicaciones web robustas con ASP.NET Core."),
                new CapacidadCurso(30),
                "Intermedio",
                "50 horas",
                249.99m,
                "https://example.com/aspnet.jpg",
                "Desarrollo Web",
                instructorId1,
                new List<Modulo>
                {
                    new Modulo(Guid.NewGuid(), "Introducción a ASP.NET Core", "Configuración del proyecto y conceptos clave.", new List<string> { "Instalación", "Middleware" }),
                    new Modulo(Guid.NewGuid(), "Controladores y Vistas", "Manejo de peticiones y renderizado de HTML.", new List<string> { "Controladores", "Vistas con Razor" })
                },
                new List<string> { "Conocimientos de C#." },
                new List<Testimonio> { Testimonio.Create("Maria Lopez", "Muy completo y práctico.", 4, "https://i.pravatar.cc/150?img=2") }
            ),
            Curso.Create(
                Guid.Parse("b4f5f5f5-f5f5-f5f5-f5f5-f5f5f5f5f5f5"),
                new NombreCurso("Python para Ciencia de Datos"),
                new DescripcionCurso("Domina Python y sus librerías para análisis de datos."),
                new CapacidadCurso(40),
                "Intermedio",
                "60 horas",
                299.99m,
                "https://example.com/python.jpg",
                "Data Science",
                instructorId2,
                new List<Modulo>
                {
                    new Modulo(Guid.NewGuid(), "Fundamentos de Python", "Sintaxis y estructuras de datos.", new List<string> { "Listas y Tuplas", "Diccionarios" }),
                    new Modulo(Guid.NewGuid(), "Pandas y NumPy", "Manipulación y análisis de datos.", new List<string> { "DataFrames", "Arrays Numéricos" })
                },
                new List<string> { "Conocimientos básicos de estadística." },
                new List<Testimonio> { Testimonio.Create("Carlos Gomez", "El mejor curso de Python que he tomado.", 5, "https://i.pravatar.cc/150?img=3") }
            ),
            Curso.Create(
                Guid.Parse("c4f5f5f5-f5f5-f5f5-f5f5-f5f5f5f5f5f5"),
                new NombreCurso("Java de Cero a Experto"),
                new DescripcionCurso("Conviértete en un desarrollador Java profesional."),
                new CapacidadCurso(50),
                "Principiante",
                "70 horas",
                199.99m,
                "https://example.com/java.jpg",
                "Programación",
                instructorId2,
                new List<Modulo>
                {
                    new Modulo(Guid.NewGuid(), "Introducción a Java", "Entorno de desarrollo y sintaxis.", new List<string> { "JDK y JRE", "Variables y Operadores" }),
                    new Modulo(Guid.NewGuid(), "Spring Framework", "Crea aplicaciones empresariales con Spring.", new List<string> { "Inyección de Dependencias", "Spring Boot" })
                },
                new List<string> { "Ninguno." },
                new List<Testimonio> { Testimonio.Create("Ana Martinez", "Excelente contenido y muy bien explicado.", 5, "https://i.pravatar.cc/150?img=4") }
            ),
            Curso.Create(
                Guid.Parse("d4f5f5f5-f5f5-f5f5-f5f5-f5f5f5f5f5f5"),
                new NombreCurso("Angular: La Guía Completa"),
                new DescripcionCurso("Crea aplicaciones web de una sola página con Angular."),
                new CapacidadCurso(35),
                "Avanzado",
                "55 horas",
                349.99m,
                "https://example.com/angular.jpg",
                "Desarrollo Web",
                instructorId1,
                new List<Modulo>
                {
                    new Modulo(Guid.NewGuid(), "Componentes y Plantillas", "La base de las aplicaciones Angular.", new List<string> { "Componentes", "Directivas" }),
                    new Modulo(Guid.NewGuid(), "Servicios y RxJS", "Manejo de estado y datos asíncronos.", new List<string> { "Servicios", "Observables" })
                },
                new List<string> { "Conocimientos de HTML, CSS y JavaScript." },
                new List<Testimonio> { Testimonio.Create("Pedro Rodriguez", "El curso es muy completo, pero un poco rápido para mi gusto.", 3, "https://i.pravatar.cc/150?img=5") }
            )
        };

        foreach (var curso in cursos)
        {
            var filter = Builders<Curso>.Filter.Eq(c => c.Id, curso.Id);
            if (cursosCollection.Find(filter).FirstOrDefault() == null)
            {
                cursosCollection.InsertOne(curso);
            }
        }
    }
}
