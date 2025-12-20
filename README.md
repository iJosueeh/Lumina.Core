# 🎓 Lumina Core - Backend Microservices 🚀

![Status](https://img.shields.io/badge/Status-Active%20Development-blue)
![License](https://img.shields.io/badge/License-MIT-green)
![.NET](https://img.shields.io/badge/.NET-8.0-purple)

## ✨ Descripción del Proyecto

**Lumina Core** es el sistema backend robusto y escalable que potencia el ecosistema educativo de la institución. Diseñado bajo una arquitectura de **microservicios** con **Clean Architecture**, gestiona de forma descentralizada los dominios de Estudiantes, Docentes, Cursos y Usuarios.

Este repositorio contiene todos los servicios backend necesarios para operar el **Lumina Core Portal** (Frontend).

## 🏗️ Arquitectura y Estado Actual

El sistema está dividido en dominios clave. A continuación se detalla el estado de implementación actual:

| Microservicio | Responsabilidad | Estado | Notas |
| :--- | :--- | :---: | :--- |
| **🔐 Usuarios.Api** | Auth, Roles, Gestión de Usuarios | ✅ **Estable** | Login con JWT funcional. Roles: Estudiante, Docente, Admin. |
| **🎓 Estudiantes.Api** | Matrículas, Progreso, Dashboard | ✅ **Estable** | Integrado con Portal Estudiante. Consultas de cursos y notas operativas. |
| **📚 Cursos.Api** | Catálogo, Contenido, Recursos | ✅ **Estable** | Gestión de cursos y materiales. Base NoSQL (MongoDB). |
| **👨‍🏫 Docentes.Api** | Gestión de Cursos, Calificaciones | 🚧 **En Progreso** | Endpoint base creados. Pendiente: `CursosImpartidos` y flujo de gestión de notas. |
| **📝 Evaluaciones.Api** | Exámenes, Tareas, Notas | 🚧 **En Progreso** | Estructura base lista. Integración con flujo docente en desarrollo. |

## 🛠️ Stack Tecnológico

*   **.NET 8 (C#):** Core del desarrollo.
*   **Clean Architecture (CQRS + MediatR):** Patrón de diseño para desacoplar capas.
*   **Bases de Datos:**
    *   **PostgreSQL:** Relacional (Usuarios, Estudiantes, Docentes).
    *   **MongoDB:** Documental (Cursos/Contenido).
*   **RabbitMQ:** Mensajería asíncrona para eventos de dominio.
*   **Docker:** (Opcional) Contenerización de servicios.

## 🚀 Guía de Inicio Rápido

### 1. Requisitos Previos
*   **.NET SDK 8.0+**
*   **PostgreSQL** (Puerto default: 5432)
*   **MongoDB** (Puerto default: 27017)
*   **RabbitMQ** (Puerto default: 5672)

### 2. Configuración (.env)
Crea un archivo `.env` en la raíz de `PlataformaAcademica` (NO en `src`).
*Nota: Este archivo es ignorado por git por seguridad.*

```env
# Bases de Datos
DB_CONNECTION_ESTUDIANTES="Host=localhost;Database=Lumina_Estudiantes;Username=postgres;Password=tu_password"
DB_CONNECTION_DOCENTES="Host=localhost;Database=Lumina_Docentes;Username=postgres;Password=tu_password"
DB_CONNECTION_USUARIOS="Host=localhost;Database=Lumina_Usuarios;Username=postgres;Password=tu_password"
MONGO_CONNECTION_STRING="mongodb://localhost:27017"
MONGO_DATABASE_NAME="Lumina_Cursos"

# Mensajería y Cache
UrlRabbit="amqp://guest:guest@localhost:5672"
DB_CONNECTION_REDIS="localhost:6379"

# Gateway / URLs Internas
UsuariosApiBaseUrl="http://localhost:5004"
DocentesApiBaseUrl="http://localhost:5002"
CursosApiBaseUrl="http://localhost:9999"
EstudiantesApiBaseUrl="http://localhost:5003"
```

### 3. Ejecución de Servicios
Recomendamos usar pestañas separadas de terminal para cada servicio o un orquestador como Tye/Docker Compose.

```bash
# Terminal 1: Usuarios (Puerto 5004)
cd src/Usuarios/Usuarios.Api
dotnet run

# Terminal 2: Estudiantes (Puerto 5003)
cd src/Estudiantes/Estudiantes.Api
dotnet run

# Terminal 3: Docentes (Puerto 5002)
cd src/Docentes/Docentes.Api
dotnet run

# Terminal 4: Cursos (Puerto 9999)
cd src/Cursos/Cursos.Api
dotnet run
```

Accede a Swagger para probar: `http://localhost:5004/swagger` (Usuarios), etc.

## 🗓️ Roadmap Inmediato (Dic 2025)
1.  **Finalizar Módulo Docente**: Implementar `GetCursosImpartidos` y registro de notas.
2.  **Integración Frontend**: Conexión total con `lumina-core-portal`.
3.  **Seguridad**: Refinar Guards y roles en Gateway.

---
*Lumina Core © 2025 - Desarrollado con ❤️ y .NET*