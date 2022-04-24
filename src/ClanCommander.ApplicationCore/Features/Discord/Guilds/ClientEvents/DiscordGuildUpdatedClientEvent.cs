namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.ClientEvents;

public class DiscordGuildUpdatedClientEvent : INotification
{
    public ulong GuildId { get; set; }
    public string BeforeName { get; set; }
    public string AfterName { get; set; }
    public ulong BeforeOwnerId { get; set; }
    public ulong AfterOwnerId { get; set; }

    public DiscordGuildUpdatedClientEvent(ulong guildId, string beforeName, string afterName, ulong beforeOwnerId, ulong afterOwnerId)
    {
        GuildId = guildId;
        BeforeName = beforeName;
        AfterName = afterName;
        BeforeOwnerId = beforeOwnerId;
        AfterOwnerId = afterOwnerId;
    }

    internal class DiscordGuildUpdatedClientEventHandler : INotificationHandler<DiscordGuildUpdatedClientEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public DiscordGuildUpdatedClientEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Handle(DiscordGuildUpdatedClientEvent notification, CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>() 
                ?? throw new NullReferenceException();

            var guild = await dbContext.DiscordGuilds.SingleOrDefaultAsync(
                x => x.GuildId == DiscordGuildId.FromUInt64(notification.GuildId), cancellationToken);

            if (guild is null)
            {
                guild = new RegisteredDiscordGuild(
                    DiscordGuildId.FromUInt64(notification.GuildId), 
                    notification.BeforeName, 
                    DiscordUserId.FromUInt64(notification.BeforeOwnerId));

                await dbContext.AddAsync(guild, cancellationToken);
            }

            if (!notification.BeforeName.Equals(notification.AfterName))
            {
                guild.UpdateGuildName(notification.AfterName);
            }

            if (!notification.BeforeOwnerId.Equals(notification.AfterOwnerId))
            {
                guild.ChangeOwner(DiscordUserId.FromUInt64(notification.AfterOwnerId));
            }

            await dbContext.SaveEntitiesAsync(cancellationToken);
        }
    }
}
