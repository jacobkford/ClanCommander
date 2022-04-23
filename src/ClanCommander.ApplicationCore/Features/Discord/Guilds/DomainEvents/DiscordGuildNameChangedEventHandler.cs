namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.DomainEvents;

internal class DiscordGuildNameChangedEventHandler : INotificationHandler<DiscordGuildNameChangedEvent>
{
    public Task Handle(DiscordGuildNameChangedEvent notification, CancellationToken cancellationToken)
    {
        // TODO: Send to log discord channel 
        return Task.CompletedTask;
    }
}
