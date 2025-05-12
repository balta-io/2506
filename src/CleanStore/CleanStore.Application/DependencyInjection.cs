using CleanStore.Application.SharedContext.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanStore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            x.AddOpenBehavior(typeof(LoggingBehavior<,>));
            x.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        return services;
    }
}