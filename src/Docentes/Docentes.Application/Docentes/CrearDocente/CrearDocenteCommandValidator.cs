using FluentValidation;

namespace Docentes.Application.Docentes.CrearDocente;

public class CrearDocenteCommandValidator : AbstractValidator<CrearDocenteCommand>
{
    public CrearDocenteCommandValidator()
    {
        RuleFor(c => c.UsuarioId)
            .NotEmpty()
            .WithMessage("El ID de usuario no puede ser vacío.");

        RuleFor(c => c.EspecialidadId.Value)
            .NotEmpty()
            .WithMessage("El ID de especialidad no puede ser vacío.");
    }
}