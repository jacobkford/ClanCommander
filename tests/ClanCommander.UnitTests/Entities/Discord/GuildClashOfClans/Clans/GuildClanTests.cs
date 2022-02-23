namespace ClanCommander.UnitTests.Entities.Discord.GuildClashOfClans.Clans;

public class GuildClanTests
{
    private readonly ClashOfClansClanId _stubClanId = ClashOfClansClanId.FromString("#9UGQ0GL");
    private readonly string _stubClanName = "PlaneClashers";
    private readonly DiscordGuildId _stubDiscordServerId = DiscordGuildId.FromUInt64(760910445686161488u);
    private readonly GuildClan _stubClan;

    private readonly ClashOfClansPlayerId _stubPlayerId = ClashOfClansPlayerId.FromString("#PQU9QLP2V");
    private readonly DiscordUserId _stubDiscordUserId = DiscordUserId.FromUInt64(339924145909399562u);

    public GuildClanTests()
    {
        _stubClan = new GuildClan(_stubClanId, _stubClanName, _stubDiscordServerId);
    }

    [Fact]
    public void Constructor_ShouldCreateGuildClan_WhenAllParametersAreValid()
    {
        // Arrange
        // Act
        var guildClan = new GuildClan(_stubClanId, _stubClanName, _stubDiscordServerId);

        // Assert
        guildClan.Should().NotBeNull();
        guildClan.ClanId.Should().Be(_stubClanId);
        guildClan.Name.Should().Be(_stubClanName);
        guildClan.GuildId.Should().Be(_stubDiscordServerId);
        guildClan.Members.Should().BeEmpty();
        guildClan.IdentiferDiscordRole.Should().Be(default);
    }

    [Theory]
    [MemberData(nameof(InvalidConstructorParameters))]
    public void Constructor_ShouldThrowException_WhenParameterIsInvalid(string clanId, string clanName, ulong discordServerId)
    {

        Invoking(() => new GuildClan(ClashOfClansClanId.FromString(clanId), clanName, DiscordGuildId.FromUInt64(discordServerId)))
            .Should().Throw<SystemException>();
    }

    [Fact]
    public void ChangeDiscordRole_ShouldBeSuccessful_WhenParameterIsValid()
    {
        // Arrange
        var roleId = 761303410200150017u;

        // Act
        _stubClan.ChangeDiscordRole(roleId);

        // Assert
        _stubClan.IdentiferDiscordRole.Should().Be(roleId);
    }

    [Fact]
    public void ChangeDiscordRole_ShouldThrowException_WhenParameterIsNotDiscordSnowflakeId()
    {
        ulong roleId = 1000u;

        Invoking(() => _stubClan.ChangeDiscordRole(roleId))
            .Should().Throw<SystemException>();
    }

    [Fact]
    public void AddClanMember_ShouldToList_WhenAllParametersAreValid()
    {
        // Arrange
        // Act
        _stubClan.AddClanMember(_stubPlayerId, _stubDiscordUserId, ClashOfClansClanRole.Member);

        // Assert
        _stubClan.Members.Should().NotBeEmpty()
            .And.ContainSingle(clanMember =>
                clanMember.MemberId == _stubPlayerId && clanMember.UserId == _stubDiscordUserId);
    }

    [Fact]
    public void AddClanMember_ShouldThrowException_WhenAddingDuplicateMember()
    {
        // Arrange
        _stubClan.AddClanMember(_stubPlayerId, _stubDiscordUserId, ClashOfClansClanRole.Member);

        // Act
        // Assert
        Invoking(() => _stubClan.AddClanMember(_stubPlayerId, _stubDiscordUserId, ClashOfClansClanRole.Member))
            .Should().Throw<SystemException>();
    }

    [Fact]
    public void RemoveClanMember_ShouldRemoveFromList_WhenAllParametersAreValid()
    {
        // Arrange
        _stubClan.AddClanMember(_stubPlayerId, _stubDiscordUserId, ClashOfClansClanRole.Member);

        // Act
        _stubClan.RemoveClanMember(_stubPlayerId);

        // Assert
        _stubClan.Members.Should().BeEmpty();
    }

    [Fact]
    public void RemoveClanMember_ShouldThrowException_WhenMemberNotExists()
    {
        Invoking(() => _stubClan.RemoveClanMember(_stubPlayerId))
            .Should().Throw<SystemException>();
    }

    public static IEnumerable<object[]> InvalidConstructorParameters =>
        new List<object[]>
        {
            new object[] { "", "PlaneClashers", 760910445686161488u },
            new object[] { "test", "PlaneClashers", 760910445686161488u },
            new object[] { "#9UGQ0GL", "", 760910445686161488u },
            new object[] { "#9UGQ0GL", "PlaneClashers", (ulong)1 }
        };
}
