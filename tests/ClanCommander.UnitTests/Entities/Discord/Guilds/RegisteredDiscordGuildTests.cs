namespace ClanCommander.UnitTests.Entities.Discord.Guilds;

public class RegisteredDiscordGuildTests
{
    private readonly DiscordGuildId _stubDiscordGuildId = DiscordGuildId.FromUInt64(760910445686161488u);
    private readonly string _stubGuildName = "PlaneClashers";
    private readonly DiscordUserId _stubGuildOwnerId = DiscordUserId.FromUInt64(339924145909399562u);
    private readonly RegisteredDiscordGuild _stubRegisteredDiscordGuild;

    public RegisteredDiscordGuildTests()
    {
        _stubRegisteredDiscordGuild = new RegisteredDiscordGuild(_stubDiscordGuildId, _stubGuildName, _stubGuildOwnerId);
    }

    [Fact]
    public void Constructor_ShouldCreateGuild_WhenAllParametersAreValid()
    {
        // Arrange
        var discordGuildId = DiscordGuildId.FromUInt64(760910445686161488u);
        var guildName = "PlaneClashers";
        var guildOwnerId = DiscordUserId.FromUInt64(339924145909399562u);

        // Act
        var newGuild = new RegisteredDiscordGuild(discordGuildId, guildName, guildOwnerId);

        // Assert
        newGuild.Should().NotBeNull();
        newGuild.GuildId.Should().Be(discordGuildId);
        newGuild.Name.Should().Be(guildName);
        newGuild.OwnerId.Should().Be(guildOwnerId);
    }

    [Theory]
    [MemberData(nameof(InvalidConstructorParameters))]
    public void Constructor_ShouldThrowException_WhenParameterIsInvalid(ulong discordGuildId, string guildName, ulong guildOwnerId)
    {
        Invoking(() => new RegisteredDiscordGuild(DiscordGuildId.FromUInt64(discordGuildId), guildName, DiscordUserId.FromUInt64(guildOwnerId)))
            .Should().Throw<SystemException>();
    }

    [Fact]
    public void UpdateGuildName_ShouldChangeName_WhenParameterIsValid()
    {
        // Arrange
        var guildName = "Test Name";

        // Act
        _stubRegisteredDiscordGuild.UpdateGuildName(guildName);

        // Assert
        _stubRegisteredDiscordGuild.Name.Should().Be(guildName);
    }

    [Fact]
    public void UpdateGuildName_ShouldThrowException_WhenEmptyStringIsProvided()
    {
        Invoking(() => _stubRegisteredDiscordGuild.UpdateGuildName(string.Empty))
            .Should().Throw<SystemException>();
    }

    [Fact]
    public void ChangeOwner_ShouldChangeOwnerId_WhenParameterIsValid()
    {
        // Arrange
        var newOwnerId = DiscordUserId.FromUInt64(751887766404726806u);

        // Act
        _stubRegisteredDiscordGuild.ChangeOwner(newOwnerId);

        // Assert
        _stubRegisteredDiscordGuild.OwnerId.Should().Be(newOwnerId);
    }

    [Fact]
    public void ChangeOwner_ShouldThrowException_WhenInvalidIdProvided()
    {
        Invoking(() => _stubRegisteredDiscordGuild.ChangeOwner(DiscordUserId.FromUInt64(0u)))
            .Should().Throw<SystemException>();
    }

    public static IEnumerable<object[]> InvalidConstructorParameters =>
        new List<object[]>
        {
            new object[] { (ulong)1, "PlaneClashers", 760910445686161488u },
            new object[] { 339924145909399562u, "", 760910445686161488u },
            new object[] { 339924145909399562u, "PlaneClashers", (ulong)1 }
        };
}
