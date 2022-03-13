namespace ClanCommander.IntegrationTests.DiscordSocketClientEvents.Users;

public class UserJoinedGuildClientEventTests : TestBase
{
    public UserJoinedGuildClientEventTests() 
        : base() { }

    [Fact]
    public async void ShouldCreateUser_WhenUserJoinsGuild_AndHasNotBeenPreviouslyRegistered()
    {
        // Arrange
        var guildMock = new ValidRegisteredDiscordGuildMock();
        await guildMock.SeedToDatabaseAsync(ServiceProvider);

        var userId = 339924145909399562u;
        var userName = "testuser#0001";

        // Act
        await Mediator.Publish(new UserJoinedGuildClientEvent(userId, userName, guildMock.GuildId.Value));

        // Assert
        await using var scope = ServiceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var result = await dbContext.DiscordUsers
            .AsNoTracking()
            .SingleAsync(x => x.UserId == DiscordUserId.FromUInt64(userId));

        result.Should().NotBeNull();
        result.UserId.Value.Should().Be(userId);
        result.Username.Should().Be(userName);
    }
}
