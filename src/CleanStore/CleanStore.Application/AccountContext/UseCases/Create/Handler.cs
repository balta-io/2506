using CleanStore.Application.AccountContext.Repositories.Abstractions;
using CleanStore.Application.SharedContext.Repositories.Abstractions;
using CleanStore.Application.SharedContext.Results;
using CleanStore.Application.SharedContext.UseCases.Abstractions;
using CleanStore.Domain.AccountContext.Entities;
using CleanStore.Domain.AccountContext.ValueObjects;

namespace CleanStore.Application.AccountContext.UseCases.Create;

public sealed class Handler(
    IAccountRepository accountRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<Command, Response>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        // Verifica se o E-mail já está cadastrado
        var emailExists = await accountRepository.VerifyEmailExistsAsync(request.Email);
        if (emailExists)
            return Result.Failure<Response>(new Error("400", "E-mail já existe"));

        // Gera os VOs
        var email = Email.Create(request.Email);

        // Gera a entidade
        var account = Account.Create(email);

        // Persiste os dados
        await accountRepository.SaveAsync(account);
        await unitOfWork.CommitAsync();

        // Retorna a resposta
        var response = new Response(account.Id, account.Email);
        return Result.Success(response);
    }
}