using CleanStore.Domain.AccountContext.Entities;
using CleanStore.Domain.SharedContext.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanStore.Infrastructure.SharedContext.Data;

public class AppDbContext(IPublisher publisher, DbContextOptions<AppDbContext> options) 
    : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        await PublishDomainEvents();
        return result;
    }

    private async Task PublishDomainEvents()
    {
        var events = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents;
                return domainEvents;
            })
            .ToList();

        foreach (var item in events)
            await publisher.Publish(item);

        foreach (var entity in ChangeTracker
                     .Entries<Entity>()
                     .Select(entry => entry.Entity)) 
            entity.ClearDomainEvents();
            
    }
}