using CleanStore.Application.SharedContext.UseCases.Abstractions;
using FluentValidation;
using MediatR;

namespace CleanStore.Application.SharedContext.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (validators.Any() == false)
            return await next();

        var context = new ValidationContext<TRequest>(request);
        var validationErrors = validators
            .Select(x => x.Validate(context))
            .Where(x => x.Errors.Any())
            .SelectMany(x => x.Errors)
            .Select(x => new ValidationError(x.PropertyName, x.ErrorMessage))
            .ToList();

        if (validationErrors.Any())
            throw new ValidationException(validationErrors);

        return await next();
    }
}

public sealed class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationError> errors) 
        => Errors = errors;

    public IEnumerable<ValidationError> Errors { get; }
}

public sealed record ValidationError(string PropertyName, string ErrorMessage);