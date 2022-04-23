namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.DomainEvents;

internal class DiscordGuildMessageCommandPrefixChangedEventHandler : INotificationHandler<DiscordGuildMessageCommandPrefixChangedEvent>
{
    public Task Handle(DiscordGuildMessageCommandPrefixChangedEvent notification, CancellationToken cancellationToken)
    {
        // TODO: Send to log discord channel 
        return Task.CompletedTask;
    }
}
