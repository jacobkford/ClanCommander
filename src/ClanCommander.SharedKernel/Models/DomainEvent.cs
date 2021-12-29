using MediatR;

namespace ClanCommander.SharedKernel.Models;

public class DomainEvent : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
