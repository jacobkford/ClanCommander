namespace ClanCommander.IntegrationTests.SeedData;

public class ValidRegisteredDiscordGuildMock
{
    internal DiscordGuildId GuildId = DiscordGuildId.FromUInt64(760910445686161488u);
    internal string GuildName = "Test1";
    internal DiscordUserId GuildOwner = DiscordUserId.FromUInt64(339924145909399562u);
    internal string GuildPrefix = "?";
    private readonly RegisteredDiscordGuild _guildEntity;
    private readonly GuildMessageCommands _guildMessageCommandsEntity;

    public ValidRegisteredDiscordGuildMock()
    {
        _guildEntity = new RegisteredDiscordGuild(GuildId, GuildName, GuildOwner);
        _guildMessageCommandsEntity = new GuildMessageCommands(GuildId);
        _guildMessageCommandsEntity.ChangeMessageCommandPrefix(GuildPrefix);
    }

    internal async Task SeedToDatabaseAsync(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.AddAsync(_guildEntity);
        await context.AddAsync(_guildMessageCommandsEntity);
        await context.SaveChangesAsync();
    }
}
