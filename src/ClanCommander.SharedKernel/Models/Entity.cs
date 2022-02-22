namespace ClanCommander.SharedKernel.Models;

// https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Services/Ordering/Ordering.Domain/SeedWork/Entity.cs
public abstract class Entity
{
    int? _requestedHashCode;

    int _id;
    public virtual int Id
    {
        get => _id;
        protected set => _id = value;
    }

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

    public bool IsTransient()
    {
        return Id == default;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || obj is not Entity)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (this.GetType() != obj.GetType())
            return false;

        Entity item = (Entity)obj;

        if (item.IsTransient() || this.IsTransient())
            return false;
        else
            return item.Id == this.Id;
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }
        else
            return base.GetHashCode();

    }
    public static bool operator ==(Entity left, Entity right)
    {
        if (Equals(left, null))
            return Equals(right, null);
        else
            return left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
}
