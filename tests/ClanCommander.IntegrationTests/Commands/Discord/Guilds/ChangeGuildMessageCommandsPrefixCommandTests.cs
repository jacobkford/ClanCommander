namespace ClanCommander.IntegrationTests.Commands.Discord.Guilds;

public class ChangeGuildMessageCommandsPrefixCommandTests : TestBase
{
    public ChangeGuildMessageCommandsPrefixCommandTests() 
        : base() { }

    [Fact]
    public async void ShouldChangeGuildMessageCommandsPrefix_WhenPrefixHasPreviouslyBeenSetup()
    {
        // Arrange
        var mock = new ValidRegisteredDiscordGuildMock();
        await mock.SeedToDatabaseAsync(ServiceProvider);

        var newPrefix = "$";

        // Act
        var result = await Mediator.Send(new ChangeGuildMessageCommandsPrefixCommand(mock.GuildId.Value, newPrefix));

        // Assert
        result.Should().NotBeNull();
        result.GuildId.Should().Be(mock.GuildId.Value);
        result.OldPrefix.Should().Be(mock.GuildPrefix);
        result.NewPrefix.Should().Be(newPrefix);
    }

    [Fact]
    public async void ShouldCreateGuildMessageCommandAndChangePrefix_WhenGuildMessageCommandEntityDoesNotExist()
    {
        // Arrange
        var mock = new PartialRegisteredDiscordGuildMock();
        await mock.SeedToDatabaseAsync(ServiceProvider);

        var newPrefix = "£";

        // Act
        var result = await Mediator.Send(new ChangeGuildMessageCommandsPrefixCommand(mock.GuildId.Value, newPrefix));

        // Assert
        result.Should().NotBeNull();
        result.GuildId.Should().Be(mock.GuildId.Value);
        result.OldPrefix.Should().Be(GuildMessageCommands.DefaultMessageCommandPrefix);
        result.NewPrefix.Should().Be(newPrefix);
    }
}
