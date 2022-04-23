namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.DomainEvents;

internal class DiscordGuildClanMemberRemovedEventHandler : INotificationHandler<DiscordGuildClanMemberRemovedEvent>
{
    public Task Handle(DiscordGuildClanMemberRemovedEvent notification, CancellationToken cancellationToken)
    {
        // TODO: Send to log discord channel 
        return Task.CompletedTask;
    }
}
