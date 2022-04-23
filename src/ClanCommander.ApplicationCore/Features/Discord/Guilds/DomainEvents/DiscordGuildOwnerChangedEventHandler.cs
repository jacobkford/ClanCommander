namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.DomainEvents;

internal class DiscordGuildOwnerChangedEventHandler : INotificationHandler<DiscordGuildOwnerChangedEvent>
{
    public Task Handle(DiscordGuildOwnerChangedEvent notification, CancellationToken cancellationToken)
    {
        // TODO: Send to log discord channel 
        return Task.CompletedTask;
    }
}
