namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.DomainEvents;

internal class DiscordGuildRegisteredEventHandler : INotificationHandler<DiscordGuildRegisteredEvent>
{
    public Task Handle(DiscordGuildRegisteredEvent notification, CancellationToken cancellationToken)
    {
        // TODO: Send to log discord channel 
        return Task.CompletedTask;
    }
}
