using System.Linq.Expressions;
using CleanStore.Application.SharedContext.Specifications;
using CleanStore.Domain.AccountContext.Entities;

namespace CleanStore.Application.AccountContext.UseCases.Get.Specifications;

public class GetByIdSpecification(Guid id) : ISpecification<Account>
{
    public Expression<Func<Account, bool>> Criteria { get; }
    
    public bool IsSatisfiedBy(Account entity) 
        => entity.Id == id;
}