using CleanStore.Application.SharedContext.UseCases.Abstractions;

namespace CleanStore.Application.AccountContext.UseCases.Create;

public sealed record Response(Guid Id, string Email) : ICommandResponse;