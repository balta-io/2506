using CleanStore.Domain.SharedContext.Events.Abstractions;

namespace CleanStore.Domain.SharedContext.Entities;

public abstract class Entity(Guid id) : IEquatable<Guid>, IEquatable<Entity>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public Guid Id { get; init; } = id;
    
    public IReadOnlyList<IDomainEvent> GetDomainEvents => _domainEvents;
    
    public void ClearDomainEvents() => _domainEvents.Clear();
    
    public void RaiseDomainEvent(IDomainEvent @event) => _domainEvents.Add(@event);
    
    public bool Equals(Entity? other)
    {
        if (other is null) return false;
        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public bool Equals(Guid other) => Id == other;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Entity)obj);
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Entity? left, Entity? right) => Equals(left, right);

    public static bool operator !=(Entity? left, Entity? right) => !Equals(left, right);
}