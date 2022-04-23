namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.DomainEvents;

internal class DiscordGuildClanIdentifierRoleChangedEventHandler : INotificationHandler<DiscordGuildClanIdentifierRoleChangedEvent>
{
    public Task Handle(DiscordGuildClanIdentifierRoleChangedEvent notification, CancellationToken cancellationToken)
    {
        // TODO: Send to log discord channel 
        return Task.CompletedTask;
    }
}
