using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Bases;

public class BaseEntityWithEvent<T> : BaseEntity<T> where T : struct
{
    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped] public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    protected void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    protected void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}