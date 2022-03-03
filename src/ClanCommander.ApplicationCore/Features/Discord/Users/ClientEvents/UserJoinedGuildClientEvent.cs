namespace ClanCommander.ApplicationCore.Features.Discord.Users.ClientEvents;

public class UserJoinedGuildClientEvent : INotification
{
    public ulong UserId { get; set; }
    public string Username { get; set; }
    public ulong GuildId { get; set; }

    public UserJoinedGuildClientEvent(ulong userId, string username, ulong guildId)
    {
        UserId = userId;
        Username = username;
        GuildId = guildId;
    }

    internal class UserJoinedGuildClientEventHandler : INotificationHandler<UserJoinedGuildClientEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDbConnection _db;

        public UserJoinedGuildClientEventHandler(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _db = new NpgsqlConnection(configuration.GetConnectionString("PostgreSQL"));
        }

        public async Task Handle(UserJoinedGuildClientEvent notification, CancellationToken cancellationToken)
        {
            var userExists = (await _db.QueryAsync<ulong>(
                $@"SELECT ""{nameof(DiscordUser)}"".""{nameof(DiscordUser.UserId)}"" 
                FROM ""{ApplicationDbContext.DISCORD_SCHEMA}"".""{nameof(DiscordUser)}"" 
                WHERE ""{nameof(DiscordUser)}"".""{nameof(DiscordUser.UserId)}"" = @UserId;",
                new
                {
                    @UserId = (decimal)notification.UserId,
                })).SingleOrDefault();

            if (userExists is not (ulong)default)
                return;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()
                ?? throw new NullReferenceException();

            var user = new DiscordUser(DiscordUserId.FromUInt64(notification.UserId), notification.Username);
            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
