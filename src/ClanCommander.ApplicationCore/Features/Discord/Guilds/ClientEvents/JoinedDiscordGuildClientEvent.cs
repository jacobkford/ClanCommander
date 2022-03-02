namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.ClientEvents;

/// <summary>
/// This event is for when the Bot joins a Discord Guild
/// </summary>
public class JoinedDiscordGuildClientEvent : INotification
{
    public ulong GuildId { get; set; }
    public string GuildName { get; set; }
    public ulong GuildOwner { get; set; }

    public JoinedDiscordGuildClientEvent(ulong guildId, string guildName, ulong guildOwner)
    {
        GuildId = guildId;
        GuildName = guildName;
        GuildOwner = guildOwner;
    }

    internal class JoinedDiscordGuildClientEventHandler : INotificationHandler<JoinedDiscordGuildClientEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public JoinedDiscordGuildClientEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Handle(JoinedDiscordGuildClientEvent notification, CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()
                ?? throw new NullReferenceException();

            var guildAlreadyExists = await dbContext.DiscordGuilds.SingleOrDefaultAsync(x => x.GuildId == DiscordGuildId.FromUInt64(notification.GuildId));

            if (guildAlreadyExists is not null)
            {
                guildAlreadyExists.UpdateGuildName(notification.GuildName);
                guildAlreadyExists.ChangeOwner(DiscordUserId.FromUInt64(notification.GuildOwner));

                await dbContext.SaveChangesAsync();
                return;
            }

            var guild = new RegisteredDiscordGuild(
                    DiscordGuildId.FromUInt64(notification.GuildId), 
                    notification.GuildName, 
                    DiscordUserId.FromUInt64(notification.GuildOwner));

            await dbContext.AddAsync(guild);
            await dbContext.SaveChangesAsync();
        }
    }
}
