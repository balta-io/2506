using CleanStore.Application.AccountContext.Repositories.Abstractions;
using CleanStore.Application.AccountContext.UseCases.Get.Specifications;
using CleanStore.Application.SharedContext.Results;
using CleanStore.Application.SharedContext.UseCases.Abstractions;

namespace CleanStore.Application.AccountContext.UseCases.Get;

public class Handler(IAccountRepository repository) : IQueryHandler<Query, Response>
{
    public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var account = await repository.GetByIdAsync(new GetByIdSpecification(request.Id));
        if (account is null)
            return Result.Failure<Response>(new Error("404", "Conta não encontrada"));
        
        return Result.Success(new Response(account.Id, account.Email));
    }
}