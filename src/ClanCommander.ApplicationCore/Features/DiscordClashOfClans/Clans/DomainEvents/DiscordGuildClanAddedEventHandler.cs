namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.DomainEvents;

internal class DiscordGuildClanAddedEventHandler : INotificationHandler<DiscordGuildClanAddedEvent>
{
    public Task Handle(DiscordGuildClanAddedEvent notification, CancellationToken cancellationToken)
    {
        // TODO: Send to log discord channel 
        return Task.CompletedTask;
    }
}
