using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public record Password
{
    public string Value { get; init; }
    public bool IsFailure { get; set; }
    public string Error { get; set; }

    public static implicit operator string(Password password) => password.Value;

    private Password(string value)
    {
        Value = value;
        IsFailure = false;
        Error = string.Empty;
    }

    public static Result<Password> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value) || value.Length < 8)
        {
            return Result.Failure<Password>(UserErrors.PasswordInvalid);
        }
        return new Password(value);
    }
}