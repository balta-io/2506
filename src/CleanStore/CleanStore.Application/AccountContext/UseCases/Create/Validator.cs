using CleanStore.Domain.AccountContext.ValueObjects;
using FluentValidation;

namespace CleanStore.Application.AccountContext.UseCases.Create;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Email)
            .MinimumLength(Email.MinLength)
            .WithMessage($"O e-mail deve conter pelo menos {Email.MinLength} caracteres");
        
        RuleFor(x => x.Email)
            .MaximumLength(Email.MaxLength)
            .WithMessage($"O e-mail deve conter no máximo {Email.MaxLength} caracteres");

        RuleFor(x => x.Email)
            .Matches(Email.Pattern)
            .WithMessage("E-mail inválido");
    }
}