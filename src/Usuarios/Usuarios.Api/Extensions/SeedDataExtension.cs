using Bogus;
using Dapper;
using Usuarios.Application.Abstractions.Data;

namespace Usuarios.Api.Extensions;

public static class SeedDataExtension
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();

        using var connection = sqlConnectionFactory.CreateConnection();

        List<object> roles;

        const string sqlContarRoles = "SELECT COUNT(*) from public.roles";

        int totalRoles = connection.ExecuteScalar<int>(sqlContarRoles);

        if (totalRoles == 0)
        {
            roles = [
                new {
                    id = Guid.NewGuid(),
                    nombre = "Teacher",
                    descripcion = "Usuario Docente"
                },
                new {
                    id = Guid.NewGuid(),
                    nombre = "Student",
                    descripcion = "Usuario Estudiante"
                },
            ];

            const string sqlInsertRoles = """
                INSERT INTO public.roles
                (id,nombre_rol,descripcion)
                values (@id,@nombre,@descripcion)
            """;

            connection.Execute(sqlInsertRoles,roles);

            // Obtener IDs de roles para usuarios de prueba
            var teacherRoleId = (Guid)((dynamic)roles[0]).id;
            var studentRoleId = (Guid)((dynamic)roles[1]).id;

            List<object> usuarios = new();

            // USUARIOS DE PRUEBA CONOCIDOS (para login)
            var testUsers = new[]
            {
                new {
                    id = Guid.NewGuid(),
                    nombre_usuario = "estudiante.test",
                    password = BCrypt.Net.BCrypt.HashPassword("Test123!"),
                    rol_id = studentRoleId,
                    nombres_persona = "Juan Carlos",
                    apellido_paterno = "Pérez",
                    apellido_materno = "García",
                    direccion_pais = "Perú",
                    direccion_departamento = "Lima",
                    direccion_provincia = "Lima",
                    direccion_distrito = "San Isidro",
                    direccion_calle = "Av. Javier Prado 123",
                    fecha_nacimiento = new DateTime(2000, 5, 15),
                    correo_electronico = "estudiante@lumina.edu",
                    estado = "Activo",
                    fecha_ultimo_cambio = DateTime.UtcNow
                },
                new {
                    id = Guid.NewGuid(),
                    nombre_usuario = "profesor.test",
                    password = BCrypt.Net.BCrypt.HashPassword("Test123!"),
                    rol_id = teacherRoleId,
                    nombres_persona = "María Elena",
                    apellido_paterno = "Rodríguez",
                    apellido_materno = "López",
                    direccion_pais = "Perú",
                    direccion_departamento = "Lima",
                    direccion_provincia = "Lima",
                    direccion_distrito = "Miraflores",
                    direccion_calle = "Av. Larco 456",
                    fecha_nacimiento = new DateTime(1985, 8, 20),
                    correo_electronico = "profesor@lumina.edu",
                    estado = "Activo",
                    fecha_ultimo_cambio = DateTime.UtcNow
                }
            };

            usuarios.AddRange(testUsers);

            // USUARIOS ALEATORIOS (para pruebas de volumen)
            for (int i = 0; i < 50 ; i++)
            {
                var fake = new Faker();
                int indexRol = fake.Random.Int(0,roles.Count()-1);
                var maximoFechaNacimiento = fake.Date.Past(50);

                var usuario = new 
                {
                    id = Guid.NewGuid(),
                    nombre_usuario = fake.Person.UserName,
                    password = BCrypt.Net.BCrypt.HashPassword(fake.Internet.Password(12)), // Contraseña hasheada
                    rol_id = (Guid)((dynamic)roles[indexRol]).id,
                    nombres_persona = fake.Person.FirstName,
                    apellido_paterno = fake.Person.LastName,
                    apellido_materno = fake.Person.LastName,
                    direccion_pais =  fake.Address.Country(),
                    direccion_departamento =  fake.Address.City(),
                    direccion_provincia = fake.Address.City(),
                    direccion_distrito = fake.Address.StreetName(),
                    direccion_calle = fake.Address.StreetAddress(),
                    fecha_nacimiento = fake.Date.Between(maximoFechaNacimiento,DateTime.UtcNow),
                    correo_electronico = fake.Person.Email,
                    estado = fake.PickRandom("Activo","Inactivo"),
                    fecha_ultimo_cambio = DateTime.UtcNow
                };

                usuarios.Add(usuario);
            }

            const string sqlInsertUsuarios = """
                INSERT INTO public.usuarios
                (id,nombre_usuario,password,rol_id,nombres_persona,apellido_paterno,apellido_materno,direccion_pais,direccion_departamento,direccion_provincia,direccion_distrito,direccion_calle,fecha_nacimiento,correo_electronico,estado,fecha_ultimo_cambio)
                values (@id,@nombre_usuario,@password,@rol_id,@nombres_persona,@apellido_paterno,@apellido_materno,@direccion_pais,@direccion_departamento,@direccion_provincia,@direccion_distrito,@direccion_calle,@fecha_nacimiento,@correo_electronico,@estado,@fecha_ultimo_cambio)
            """;

            connection.Execute(sqlInsertUsuarios,usuarios);

        }
    }
}
