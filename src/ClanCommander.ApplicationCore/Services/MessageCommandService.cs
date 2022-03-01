namespace ClanCommander.ApplicationCore.Services;

internal class MessageCommandService : IMessageCommandService
{
    private readonly IDbConnection _db;
    private readonly ICacheService _cacheService;

    public MessageCommandService(IConfiguration configuration, ICacheService cacheService)
    {
        _db = new NpgsqlConnection(configuration.GetConnectionString("PostgreSQL"));
        _cacheService = cacheService;
    }

    public async Task<string?> GetGuildPrefixAsync(ulong? guildId)
    {
        if (guildId is null)
        {
            throw new ArgumentNullException(nameof(guildId));
        }

        var cachePrefix = await _cacheService.GetAsync<string>($"discord:guild:{guildId}:prefix");

        if (cachePrefix is not null)
        {
            return cachePrefix;
        }

        var dbPrefix = (await _db.QueryAsync<string>(
            $@"SELECT ""{nameof(GuildMessageCommands.MessageCommandPrefix)}"" 
            FROM ""{ApplicationDbContext.DISCORD_SCHEMA}"".""{nameof(GuildMessageCommands)}"" 
            WHERE ""{nameof(GuildMessageCommands)}"".""{nameof(GuildMessageCommands.GuildId)}"" = @GuildId;",
            new
            {
                @GuildId = (decimal)guildId
            })).SingleOrDefault() ?? GuildMessageCommands.DefaultMessageCommandPrefix;

        await _cacheService.SetAsync($"discord:guild:{guildId}:prefix", dbPrefix, TimeSpan.FromDays(30), TimeSpan.FromDays(2));

        return dbPrefix;
    }
}
