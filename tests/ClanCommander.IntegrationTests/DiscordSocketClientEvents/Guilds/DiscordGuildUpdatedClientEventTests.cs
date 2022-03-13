namespace ClanCommander.IntegrationTests.DiscordSocketClientEvents.Guilds;

public class DiscordGuildUpdatedClientEventTests : TestBase
{
    public DiscordGuildUpdatedClientEventTests() 
        : base() { }

    [Fact]
    public async void ShouldUpdateRegisteredGuild_WhenChangesAreMade()
    {
        // Arrange
        var mock = new ValidRegisteredDiscordGuildMock();
        await mock.SeedToDatabaseAsync(ServiceProvider);

        var newName = "Test2";
        var newOwner = 751887766404726806u;

        // Act
        await Mediator.Publish(new DiscordGuildUpdatedClientEvent(
            mock.GuildId.Value,
            mock.GuildName,
            newName,
            mock.GuildOwner.Value,
            newOwner));

        // Assert
        await using var scope = ServiceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var result = await dbContext.DiscordGuilds
            .AsNoTracking()
            .SingleAsync(x => x.GuildId == mock.GuildId);

        result.Should().NotBeNull();
        result.GuildId.Should().Be(mock.GuildId);
        result.Name.Should().Be(newName);
        result.OwnerId.Value.Should().Be(newOwner);
    }

    [Fact]
    public async void ShouldCreateAndUpdateRegisteredGuild_WhenChangesAreMade_AndGuildHasNotBeenRegistered()
    {
        // Arrange
        var guildId = 236523452230533121u;
        var guildOldName = "Test3";
        var guildOldOwner = 155149108183695360u;
        var guildNewName = "Test4";
        var newOwner = 339924145909399562u;

        // Act
        await Mediator.Publish(new DiscordGuildUpdatedClientEvent(
            guildId, guildOldName, guildNewName, guildOldOwner, newOwner));

        // Assert
        await using var scope = ServiceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var result = await dbContext.DiscordGuilds
            .AsNoTracking()
            .SingleAsync(x => x.GuildId == DiscordGuildId.FromUInt64(guildId));

        result.Should().NotBeNull();
        result.GuildId.Value.Should().Be(guildId);
        result.Name.Should().Be(guildNewName);
        result.OwnerId.Value.Should().Be(newOwner);
    }
}
