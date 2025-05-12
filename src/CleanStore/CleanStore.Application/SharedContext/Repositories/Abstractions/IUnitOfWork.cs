namespace CleanStore.Application.SharedContext.Repositories.Abstractions;

public interface IUnitOfWork
{
    Task CommitAsync();
    Task RollbackAsync();
}