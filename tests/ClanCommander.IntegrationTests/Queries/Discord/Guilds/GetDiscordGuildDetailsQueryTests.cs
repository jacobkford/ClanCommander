namespace ClanCommander.IntegrationTests.Queries.Discord.Guilds;

public class GetDiscordGuildDetailsQueryTests : TestBase
{
    [Fact]
    public async void ShouldReturnGuildWithMessageCommandsPrefix()
    {
        // Arrange
        var mock = new ValidRegisteredDiscordGuildMock();
        await mock.SeedToDatabaseAsync(ServiceProvider);

        // Act
        var result = await Mediator.Send(new GetDiscordGuildDetailsQuery(mock.GuildId.Value));

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(mock.GuildId.Value);
        result.Name.Should().Be(mock.GuildName);
        result.OwnerId.Should().Be(mock.GuildOwner.Value);
        result.MessageCommandPrefix.Should().Be(mock.GuildPrefix);
    }

    [Fact]
    public async void ShouldReturnGuild_WhenMessageCommandsPrefixNotCreated()
    {
        // Arrange
        var mock = new PartialRegisteredDiscordGuildMock();
        await mock.SeedToDatabaseAsync(ServiceProvider);

        // Act
        var result = await Mediator.Send(new GetDiscordGuildDetailsQuery(mock.GuildId.Value));

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(mock.GuildId.Value);
        result.Name.Should().Be(mock.GuildName);
        result.OwnerId.Should().Be(mock.GuildOwner.Value);
        result.MessageCommandPrefix.Should().BeNull();
    }
}
