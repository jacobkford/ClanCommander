namespace ClanCommander.UnitTests.Entities.Discord.MessageCommands;

public class GuildMessageCommandsTests
{
    private readonly DiscordGuildId _stubDiscordGuildId = DiscordGuildId.FromUInt64(760910445686161488u);
    private readonly GuildMessageCommands _stubMessageCommandsConfig;

    public GuildMessageCommandsTests()
    {
        _stubMessageCommandsConfig = new GuildMessageCommands(_stubDiscordGuildId);
    }

    [Fact]
    public void Constructor_ShouldCreateMessageCommandsConfigurationWithADefaultPrefix_WhenParameterIsValid()
    {
        // Arrange
        var discordGuildId = DiscordGuildId.FromUInt64(760910445686161488u);

        // Act
        var messageCommandsConfig = new GuildMessageCommands(discordGuildId);

        // Assert
        messageCommandsConfig.Should().NotBeNull();
        messageCommandsConfig.GuildId.Should().Be(discordGuildId);
        messageCommandsConfig.MessageCommandPrefix.Should().Be("!");
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenParameterIsInvalid()
    {
        Invoking(() => new GuildMessageCommands(DiscordGuildId.FromUInt64(1)))
            .Should().Throw<SystemException>();
    }

    [Fact]
    public void ChangeMessageCommandPrefix_ShouldChangePrefix_WhenParameterIsValid()
    {
        // Arrange
        // Act
        _stubMessageCommandsConfig.ChangeMessageCommandPrefix("?");

        // Assert
        _stubMessageCommandsConfig.MessageCommandPrefix.Should().Be("?");
    }

    [Fact]
    public void ChangeMessageCommandPrefix_ShouldThrowException_WhenParameterIsInvalid()
    {
        Invoking(() => _stubMessageCommandsConfig.ChangeMessageCommandPrefix(string.Empty))
            .Should().Throw<SystemException>();
    }
}
