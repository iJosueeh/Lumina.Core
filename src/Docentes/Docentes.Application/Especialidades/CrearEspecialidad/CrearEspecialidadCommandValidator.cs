using FluentValidation;

namespace Docentes.Application.Especialidades.CrearEspecialidad;

public class CrearEspecialidadCommandValidator : AbstractValidator<CrearEspecialidadCommand>
{
    public CrearEspecialidadCommandValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre debe tener máximo 100 caracteres.");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripción es obligatoria.")
            .MaximumLength(500).WithMessage("La descripción debe tener máximo 500 caracteres.");
    }
}