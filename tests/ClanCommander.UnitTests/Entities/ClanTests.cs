using ClanCommander.ApplicationCore.Entities.ClanAggregate;

namespace ClanCommander.UnitTests.Entities;

public class ClanTests
{
    private readonly string _clanId = "#9UGQ0GL";
    private readonly string _clanName = "PlaneClashers";
    private readonly ulong _discordServerId = 760910445686161488u;
    private readonly Clan _clan;

    public ClanTests()
    {
        _clan = new Clan(
            ClanId.FromString(_clanId), 
            _clanName, 
            DiscordServerId.FromUInt64(_discordServerId));
    }


    [Fact]
    public void Constructor_ShouldCreateClan_WhenAllParametersAreValid()
    {
        // Arrange
        var clanId = "#9UGQ0GL";
        var clanName = "PlaneClashers";
        var discordServerId = 760910445686161488u;

        // Act
        var result = new Clan(
            ClanId.FromString(clanId), 
            clanName, 
            DiscordServerId.FromUInt64(discordServerId));

        // Assert
        result.Should().NotBeNull();
        result.Id.Value.Should().Be(clanId);
        result.Name.Should().Be(clanName);
        result.DiscordServerId.Value.Should().Be(discordServerId);
        result.Members.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(InvalidConstructorParameters))]
    public void Constructor_ShouldThrowException_WhenParameterIsInvalid(string clanId, string clanName, ulong discordServerId)
    {
        Invoking(() => new Clan(
            ClanId.FromString(clanId), 
            clanName,
            DiscordServerId.FromUInt64(discordServerId)))
                .Should().Throw<SystemException>();
    }

    [Fact]
    public void AddClanMember_ShouldAddMemberToList_WhenAllParametersAreValid()
    {
        // Arrange
        var memberId = "#PQU9QLP2V";
        var userId = UserId.FromUInt64(339924145909399562u);

        // Act
        _clan.AddClanMember(memberId, userId);

        // Assert
        _clan.Members.Should().NotBeEmpty()
            .And.ContainSingle(clanMember =>
                clanMember.Id == memberId && clanMember.UserId == userId);
    }

    [Fact]
    public void AddClanMember_ShouldThrowException_WhenAddingDuplicateMember()
    {
        // Arrange
        var memberId = "#PQU9QLP2V";
        var userId = UserId.FromUInt64(339924145909399562u);
        _clan.AddClanMember(memberId, userId);

        // Act
        // Assert
        Invoking(() => _clan.AddClanMember(memberId, userId))
            .Should().Throw<ArgumentException>();
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
