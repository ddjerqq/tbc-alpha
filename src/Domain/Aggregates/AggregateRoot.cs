using Domain.Entities;
using Domain.Events;

namespace Domain.Aggregates;

public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id), IAggregateRoot<TId>
    where TId : struct, IEquatable<TId>
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}