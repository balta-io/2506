using CleanStore.Application.SharedContext.UseCases.Abstractions;

namespace CleanStore.Application.AccountContext.UseCases.Get;

public record Response(Guid Id, string Email) : IQueryResponse;