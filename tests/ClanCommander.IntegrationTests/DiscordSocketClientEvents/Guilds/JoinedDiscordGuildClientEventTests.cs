namespace ClanCommander.IntegrationTests.DiscordSocketClientEvents.Guilds;

public class JoinedDiscordGuildClientEventTests : TestBase
{
    public JoinedDiscordGuildClientEventTests() 
        : base() { }

    [Fact]
    public async void ShouldRegisterDiscordGuild_WhenBotJoinsGuild()
    {
        // Arrange
        var guildId = 236523452230533121u;
        var guildName = "Test3";
        var guildOwner = 155149108183695360u;

        // Act
        await Mediator.Publish(new JoinedDiscordGuildClientEvent(guildId, guildName, guildOwner));

        // Assert
        await using var scope = ServiceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var result = await dbContext.DiscordGuilds
            .AsNoTracking()
            .SingleAsync(x => x.GuildId == DiscordGuildId.FromUInt64(guildId));

        result.Should().NotBeNull();
        result.GuildId.Value.Should().Be(guildId);
        result.Name.Should().Be(guildName);
        result.OwnerId.Value.Should().Be(guildOwner);
    }
}
