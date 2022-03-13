namespace ClanCommander.IntegrationTests.DiscordSocketClientEvents.Guilds;

public class LeftDiscordGuildClientEventTests : TestBase
{
    public LeftDiscordGuildClientEventTests() 
        : base() { }

    [Fact]
    public async void ShouldDeleteRegisteredDiscordGuild_WhenBotHasLeftServer()
    {
        // Arrange
        var mock = new ValidRegisteredDiscordGuildMock();
        await mock.SeedToDatabaseAsync(ServiceProvider);

        // Act
        await Mediator.Publish(new LeftDiscordGuildClientEvent(mock.GuildId.Value));

        // Assert
        await using var scope = ServiceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var result = await dbContext.DiscordGuilds
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.GuildId == mock.GuildId);

        result.Should().BeNull();
    }
}
