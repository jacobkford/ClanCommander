namespace ClanCommander.ApplicationCore.Features.Discord.Users.DomainEvents;

internal class DiscordUserCreatedEventHandler : INotificationHandler<DiscordUserCreatedEvent>
{
    public Task Handle(DiscordUserCreatedEvent notification, CancellationToken cancellationToken)
    {
        // TODO: Send to log discord channel 
        return Task.CompletedTask;
    }
}
