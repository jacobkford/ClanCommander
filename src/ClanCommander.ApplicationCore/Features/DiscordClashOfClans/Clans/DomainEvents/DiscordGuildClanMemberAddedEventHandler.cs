namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.DomainEvents;

internal class DiscordGuildClanMemberAddedEventHandler : INotificationHandler<DiscordGuildClanMemberAddedEvent>
{
    public Task Handle(DiscordGuildClanMemberAddedEvent notification, CancellationToken cancellationToken)
    {
        // TODO: Send to log discord channel 
        return Task.CompletedTask;
    }
}
