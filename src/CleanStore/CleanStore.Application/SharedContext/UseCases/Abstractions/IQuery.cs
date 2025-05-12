using CleanStore.Application.SharedContext.Results;
using MediatR;

namespace CleanStore.Application.SharedContext.UseCases.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> where TResponse : IQueryResponse;