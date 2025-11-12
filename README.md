# 🎓 Plataforma Académica 🚀

![Status](https://img.shields.io/badge/Status-Under%20Development-yellow)
![License](https://img.shields.io/badge/License-MIT-blue)

## ✨ Descripción del Proyecto

Este proyecto es una **Plataforma Académica** robusta, diseñada con una arquitectura de **microservicios** para ofrecer escalabilidad y flexibilidad. Su objetivo es gestionar de manera eficiente los procesos educativos, incluyendo la administración de cursos, docentes, estudiantes y usuarios.

## 🛠️ Tecnologías Clave

La plataforma está construida sobre un stack tecnológico moderno y potente:

*   **.NET (C#):** Framework principal para el desarrollo de los servicios.
*   **ASP.NET Core:** Para la creación de APIs web de alto rendimiento.
*   **Entity Framework Core:** ORM para la interacción con bases de datos relacionales.
*   **PostgreSQL:** Base de datos relacional utilizada por los servicios de Docentes, Estudiantes y Usuarios.
*   **MongoDB:** Base de datos NoSQL para el servicio de Cursos.
*   **RabbitMQ:** Broker de mensajes para la comunicación asíncrona entre microservicios.
*   **Swagger/OpenAPI:** Para la documentación interactiva y prueba de las APIs.
*   **DotNetEnv:** Gestión de variables de entorno a través de archivos `.env`.

## 🚀 Configuración del Entorno de Desarrollo

Para poner en marcha la plataforma en tu entorno local, sigue estos pasos:

### 1. Requisitos Previos

Asegúrate de tener instaladas las siguientes herramientas y servicios:

*   **.NET SDK:** Versión 8.0 o superior.
*   **PostgreSQL:** Servidor de base de datos relacional.
*   **MongoDB:** Servidor de base de datos NoSQL.
*   **RabbitMQ:** Servidor de mensajería.

#### 🐰 Instalación de RabbitMQ (sin Docker)

Si prefieres no usar Docker, puedes instalar RabbitMQ y su dependencia Erlang directamente en tu sistema Windows:

1.  **Instalar Erlang:**
    *   Descarga la versión de Windows de 64 bits desde [https://www.erlang.org/downloads](https://www.erlang.org/downloads).
    *   Ejecuta el instalador y sigue las instrucciones (opciones por defecto suelen ser suficientes).
2.  **Instalar RabbitMQ Server:**
    *   Descarga el instalador para Windows desde [https://www.rabbitmq.com/install-windows.html](https://www.rabbitmq.com/install-windows.html).
    *   Ejecuta el instalador. RabbitMQ se instalará como un servicio de Windows y se iniciará automáticamente.
3.  **Habilitar Plugin de Administración (Opcional pero Recomendado):**
    *   Abre "RabbitMQ Command Prompt" como **administrador**.
    *   Ejecuta: `rabbitmq-plugins enable rabbitmq_management`
    *   **Reinicia el servicio de RabbitMQ:**
        ```bash
        net stop RabbitMQ
        net start RabbitMQ
        ```
    *   Accede a la interfaz de administración en tu navegador: `http://localhost:15672` (usuario: `guest`, contraseña: `guest`).

### 2. Archivo de Variables de Entorno (`.env`)

Crea un archivo llamado `.env` en la **raíz de la carpeta `PlataformaAcademica`** con las siguientes variables. Este archivo es crucial para la configuración de las conexiones a las bases de datos y otros servicios.

⚠️ **¡Importante!** Este archivo `.env` contiene credenciales sensibles y **NO DEBE SUBIRSE A TU REPOSITORIO GIT**. Ya está configurado en `.gitignore` para ser ignorado.

```env
DB_CONNECTION_ESTUDIANTES="<Tu cadena de conexión PostgreSQL para Estudiantes>"
DB_CONNECTION_DOCENTES="<Tu cadena de conexión PostgreSQL para Docentes>"
DB_CONNECTION_USUARIOS="<Tu cadena de conexión PostgreSQL para Usuarios>"
MONGO_CONNECTION_STRING="<Tu cadena de conexión MongoDB>"
MONGO_DATABASE_NAME="<Tu nombre de base de datos MongoDB>"
UrlRabbit="<Tu URL de conexión a RabbitMQ, ej: amqp://guest:guest@localhost:5672>"
DB_CONNECTION_REDIS="<Tu cadena de conexión Redis, ej: localhost:6379>"
UsuariosApiBaseUrl="http://localhost:5004"
CursosApiBaseUrl="http://localhost:9999"
DocentesApiBaseUrl="http://localhost:5002"
GRAYLOG_HOST="graylog"
```
Asegúrate de reemplazar los valores entre `< >` con tus credenciales y configuraciones reales.

## ▶️ Ejecución de los Microservicios

Para iniciar cada microservicio, abre una **nueva terminal** para cada uno, navega a la carpeta del proyecto API correspondiente y ejecuta `dotnet run`.

### 1. 📚 Cursos.Api

*   **Ruta:** `PlataformaAcademica/src/Cursos/Cursos.Api`
*   **Comando:** `dotnet run`
*   **Swagger UI (API REST):** `http://localhost:9999/swagger`
*   **gRPC:** `http://localhost:5001`

### 2. 👨‍🏫 Docentes.Api

*   **Ruta:** `PlataformaAcademica/src/Docentes/Docentes.Api`
*   **Comando:** `dotnet run`
*   **Swagger UI:** `http://localhost:5002/swagger`

### 3. 🧑‍🎓 Estudiantes.Api

*   **Ruta:** `PlataformaAcademica/src/Estudiantes/Estudiantes.Api`
*   **Comando:** `dotnet run`
*   **Swagger UI:** `http://localhost:5003/swagger`

### 4. 👤 Usuarios.Api

*   **Ruta:** `PlataformaAcademica/src/Usuarios/Usuarios.Api`
*   **Comando:** `dotnet run`
*   **Swagger UI:** `http://localhost:5004/swagger`

## 🤝 Contribución

¡Las contribuciones son bienvenidas! Si deseas mejorar este proyecto, por favor, sigue estos pasos:
1.  Haz un "fork" del repositorio.
2.  Crea una nueva rama (`git checkout -b feature/nueva-funcionalidad`).
3.  Realiza tus cambios y haz "commit" (`git commit -m 'feat: Añadir nueva funcionalidad'`).
4.  Sube tus cambios a tu "fork" (`git push origin feature/nueva-funcionalidad`).
5.  Abre un "Pull Request".

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Consulta el archivo `LICENSE` para más detalles.