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

        public async Task Handle(LeftDiscordGuildClientEvent notification, CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()
                ?? throw new NullReferenceException();

            var guild = await dbContext.DiscordGuilds.SingleOrDefaultAsync(x => x.GuildId == DiscordGuildId.FromUInt64(notification.GuildId));

            if (guild is not null)
            {
                dbContext.Remove(guild);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
