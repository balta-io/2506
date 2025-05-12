using CleanStore.Application.SharedContext.Results;
using MediatR;

namespace CleanStore.Application.SharedContext.UseCases.Abstractions;

public interface ICommand : IRequest<Result>;

public interface ICommand<TCommandResponse> : IRequest<Result<TCommandResponse>> where TCommandResponse : ICommandResponse;