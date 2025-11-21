using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public static class UserErrors
{
    public static Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "Credenciales inválidas.");

    public static Error PasswordInvalid = new(
        "User.PasswordInvalid",
        "El password es inválido.");
}