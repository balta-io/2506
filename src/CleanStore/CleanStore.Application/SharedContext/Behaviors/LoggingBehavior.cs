using CleanStore.Application.SharedContext.UseCases.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanStore.Application.SharedContext.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation($"Handling request: {request.GetType().Name}");
            var result = await next();
            logger.LogInformation($"Request {request.GetType().Name} processed");
            return result;
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Error while handling request: {request.GetType().Name}");
            throw;
        }
    }
}