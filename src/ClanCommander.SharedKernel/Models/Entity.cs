namespace ClanCommander.SharedKernel.Models;

public class Entity
{
    private List<DomainEvent>? _domainEvents;
    public IReadOnlyCollection<DomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(DomainEvent eventItem)
    {
        _domainEvents ??= new List<DomainEvent>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(DomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}
