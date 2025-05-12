using CleanStore.Application.SharedContext.UseCases.Abstractions;

namespace CleanStore.Application.AccountContext.UseCases.Create;

public sealed record Command(string Email) : ICommand<Response>;