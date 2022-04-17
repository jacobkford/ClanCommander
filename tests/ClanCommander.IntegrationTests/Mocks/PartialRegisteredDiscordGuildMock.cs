namespace ClanCommander.IntegrationTests.Mocks;

public class PartialRegisteredDiscordGuildMock
{
    internal DiscordGuildId GuildId = DiscordGuildId.FromUInt64(834837883810349098u);
    internal string GuildName = "Test2";
    internal DiscordUserId GuildOwner = DiscordUserId.FromUInt64(751887766404726806u);
    private readonly RegisteredDiscordGuild _guildEntity;

    public PartialRegisteredDiscordGuildMock()
    {
        _guildEntity = new RegisteredDiscordGuild(GuildId, GuildName, GuildOwner);
    }

    internal async Task SeedToDatabaseAsync(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.AddAsync(_guildEntity);
        await context.SaveChangesAsync();
    }
}
