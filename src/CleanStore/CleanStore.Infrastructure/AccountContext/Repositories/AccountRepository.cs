using CleanStore.Application.AccountContext.Repositories.Abstractions;
using CleanStore.Application.AccountContext.UseCases.Get.Specifications;
using CleanStore.Domain.AccountContext.Entities;
using CleanStore.Infrastructure.SharedContext.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanStore.Infrastructure.AccountContext.Repositories;

public class AccountRepository(AppDbContext context) : IAccountRepository
{
    public async Task<bool> VerifyEmailExistsAsync(string email)
        => await context.Accounts.AsNoTracking().AnyAsync(e => e.Email.Address == email);

    public async Task SaveAsync(Account account)
        => await context.Accounts.AddAsync(account);

    public async Task<Account?> GetByIdAsync(GetByIdSpecification specification) 
        => await context
            .Accounts
            .AsNoTracking()
            .Where(specification.Criteria)
            .FirstOrDefaultAsync();
}