using CleanStore.Application.SharedContext.Results;
using MediatR;

namespace CleanStore.Application.SharedContext.UseCases.Abstractions;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
    where TResponse : ICommandResponse
{
}