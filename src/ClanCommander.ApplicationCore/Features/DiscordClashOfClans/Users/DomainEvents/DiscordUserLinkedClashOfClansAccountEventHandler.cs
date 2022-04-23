namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Users.DomainEvents;

internal class DiscordUserLinkedClashOfClansAccountEventHandler : INotificationHandler<DiscordUserLinkedClashOfClansAccountEvent>
{
    public Task Handle(DiscordUserLinkedClashOfClansAccountEvent notification, CancellationToken cancellationToken)
    {
        // TODO: Send to log discord channel 
        return Task.CompletedTask;
    }
}
