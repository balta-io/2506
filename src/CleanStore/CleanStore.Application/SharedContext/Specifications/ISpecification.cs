namespace CleanStore.Application.SharedContext.Specifications;

public interface ISpecification<in TEntity>
{
    bool IsSatisfiedBy(TEntity entity);
}