namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.DomainEvents;

internal class DiscordGuildMessageCommandPrefixChangedEventHandler : INotificationHandler<DiscordGuildMessageCommandPrefixChangedEvent>
{
    private readonly ICacheService _cacheService;

    public DiscordGuildMessageCommandPrefixChangedEventHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task Handle(DiscordGuildMessageCommandPrefixChangedEvent notification, CancellationToken cancellationToken)
    {
        await _cacheService.SetAsync($"discord:guild:{notification.GuildId}:prefix", notification.NewPrefix, TimeSpan.FromDays(30), TimeSpan.FromDays(2));

        // TODO: Send to log discord channel 
    }
}
