namespace ClanCommander.IntegrationTests.Commands.Discord.Guilds;

public class RegisterDiscordGuildCommandTests : TestBase
{
    [Fact]
    public async void ShouldRegisterDiscordGuild()
    {
        // Arrange
        var mock = new ValidRegisteredDiscordGuildMock();

        // Act
        var result = await Mediator.Send(new RegisterDiscordGuildCommand(mock.GuildId.Value, mock.GuildName, mock.GuildOwner.Value));

        // Assert
        result.Should().NotBeNull();
        result.GuildId.Should().Be(mock.GuildId.Value);
    }

    [Fact]
    public async void ShouldThrowException_WhenDiscordGuildIsAlreadyRegistered()
    {
        // Arrange
        var mock = new ValidRegisteredDiscordGuildMock();
        await mock.SeedToDatabaseAsync(ServiceProvider);

        // Act
        // Assert
        await Invoking(async () => await Mediator.Send(new RegisterDiscordGuildCommand(mock.GuildId.Value, mock.GuildName, mock.GuildOwner.Value)))
            .Should().ThrowAsync<ArgumentException>().WithMessage("Discord server is already registered");
    }
}
