using CleanStore.Application.SharedContext.UseCases.Abstractions;

namespace CleanStore.Application.AccountContext.UseCases.Get;

public record Query(Guid Id) : IQuery<Response>;