namespace ClanCommander.IntegrationTests.DiscordSocketClientEvents.Guilds;

public class JoinedDiscordGuildClientEventTests : TestBase
{
    public JoinedDiscordGuildClientEventTests() 
        : base() { }

    [Fact]
    public async void ShouldRegisterDiscordGuild_WhenBotJoinsGuild()
    {
        // Arrange
        var mock = new ValidRegisteredDiscordGuildMock();

        // Act
        await Mediator.Publish(new JoinedDiscordGuildClientEvent(mock.GuildId.Value, mock.GuildName, mock.GuildOwner.Value));

        // Assert
        await using var scope = ServiceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var result = await dbContext.DiscordGuilds
            .AsNoTracking()
            .SingleAsync(x => x.GuildId == mock.GuildId);

        result.Should().NotBeNull();
        result.GuildId.Should().Be(mock.GuildId);
        result.Name.Should().Be(mock.GuildName);
        result.OwnerId.Value.Should().Be(mock.GuildOwner.Value);
    }
}
