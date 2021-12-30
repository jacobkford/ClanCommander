namespace ClanCommander.UnitTests.Entities;

public class DiscordServerTests
{
    private readonly ulong _serverId = 760910445686161488u;
    private readonly string _serverName = "Test Server";
    private readonly DiscordServer _discordServer;

    public DiscordServerTests()
    {
        _discordServer = new DiscordServer(_serverId, _serverName);
    }

    [Fact]
    public void Constructor_ShouldCreateDiscordServer_WhenAllParametersAreValid()
    {
        // Arrange
        var discordServerId = 760910445686161488u;
        var discordServerName = "Test Server";

        // Act
        var result = new DiscordServer(discordServerId, discordServerName);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(discordServerId);
        result.Name.Should().Be(discordServerName);
        result.Prefix.Should().BeNull();
        result.MessageCommandsEnabled.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(InvalidConstructorParameters))]
    public void Constructor_ShouldThrowException_WhenParameterIsInvalid(ulong serverId, string serverName)
    {
        Invoking(() => new DiscordServer(serverId, serverName))
            .Should().Throw<SystemException>();
    }

    [Fact]
    public void CreateServerClan_ShouldCreateClan_WhenAllParametersAreValid()
    {
        // Arrange
        var clanId = "#9UGQ0GL";
        var clanName = "PlaneClashers";

        // Act
        var result = _discordServer.CreateServerClan(clanId, clanName);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(clanId);
        result.Name.Should().Be(clanName);
        result.DiscordServerId.Should().Be(_serverId);
    }

    [Theory]
    [MemberData(nameof(InvalidCreateServerClanParameters))]
    public void CreateServerClan_ShouldThrowException_WhenParameterIsInvalid(string clanId, string clanName)
    {
        Invoking(() => _discordServer.CreateServerClan(clanId, clanName))
            .Should().Throw<SystemException>();
    }

    [Fact]
    public void UpdatePrefix_ShouldSetPrefix_WhenParameterIsValid()
    {
        // Arrange
        var prefix = "!";

        // Act
        _discordServer.UpdatePrefix(prefix);

        // Assert
        _discordServer.Prefix.Should().NotBeNull();
        _discordServer.Prefix.Should().Be(prefix);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdatePrefix_ShouldThrowException_WhenParameterIsInvalid(string prefix)
    {
        Invoking(() => _discordServer.UpdatePrefix(prefix))
            .Should().Throw<SystemException>();
    }

    [Fact]
    public void EnableMessageCommands_ShouldSetToTrue_WhenPrefixExists()
    {
        // Arrange
        _discordServer.UpdatePrefix("!");

        // Act
        _discordServer.EnableMessageCommands();

        // Assert
        _discordServer.MessageCommandsEnabled.Should().BeTrue();
    }

    [Fact]
    public void EnableMessageCommands_ShouldThrowException_WhenPrefixHasntBeenSet()
    {
        Invoking(() => _discordServer.EnableMessageCommands())
            .Should().Throw<SystemException>();
    }

    [Fact]
    public void DisableMessageCommands_ShouldSetToFalse()
    {
        // Arrange
        _discordServer.UpdatePrefix("!");
        _discordServer.EnableMessageCommands();

        // Act
        _discordServer.DisableMessageCommands();

        // Assert
        _discordServer.MessageCommandsEnabled.Should().BeFalse();
    }

    public static IEnumerable<object[]> InvalidConstructorParameters =>
        new List<object[]>
        {
            new object[] { (ulong)1, "Test Server" },
            new object[] { 760910445686161488u, "" }
        };

    public static IEnumerable<object[]> InvalidCreateServerClanParameters =>
        new List<object[]>
        {
            new object[] { "", "PlaneClashers" },
            new object[] { "#9UGQ0GL", "" }
        };
}
