namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.ClientEvents;

/// <summary>
/// This event is for when the Bot leaves a Discord Guild
/// </summary>
public class LeftDiscordGuildClientEvent : INotification
{
    public ulong GuildId { get; set; }

    public LeftDiscordGuildClientEvent(ulong guildId)
    {
        GuildId = guildId;
    }

    internal class LeftDiscordGuildClientEventHandler : INotificationHandler<LeftDiscordGuildClientEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public LeftDiscordGuildClientEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task Handle(LeftDiscordGuildClientEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Delete all Clash Of Clans related Guild data
            return Task.CompletedTask;
        }
    }
}
