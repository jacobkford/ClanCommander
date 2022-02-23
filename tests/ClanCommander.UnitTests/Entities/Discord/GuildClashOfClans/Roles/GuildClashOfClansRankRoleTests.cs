using ClanCommander.ApplicationCore.Entities.Discord.GuildClashOfClans.Roles;
using ClanCommander.ApplicationCore.Entities.Guild;

namespace ClanCommander.UnitTests.Entities.Discord.GuildClashOfClans.Roles;
public class GuildClashOfClansRankRoleTests
{
    private readonly DiscordGuildId _stubDiscordServerId = DiscordGuildId.FromUInt64(760910445686161488u);
    private readonly ulong _stubRoleId = 339924145909399562u;
    private readonly GuildClashOfClansRankRole _stubRankRole;

    public GuildClashOfClansRankRoleTests()
    {
        _stubRankRole = new GuildClashOfClansRankRole(_stubDiscordServerId, _stubRoleId, ClashOfClansClanRole.CoLeader);
    }

    [Fact]
    public void Constructor_ShouldCreateRankRole_WhenAllParametersAreValid()
    {
        // Arrange
        // Act
        var role = new GuildClashOfClansRankRole(_stubDiscordServerId, _stubRoleId, ClashOfClansClanRole.CoLeader);

        // Assert
        role.Should().NotBeNull();
        role.GuildId.Should().Be(_stubDiscordServerId);
        role.DiscordRoleId.Should().Be(_stubRoleId);
        role.InGameRole.Should().Be(ClashOfClansClanRole.CoLeader);

    }

    [Theory]
    [MemberData(nameof(InvalidConstructorParameters))]
    public void Constructor_ShouldThrowException_WhenParametersAreInvalid(ulong guildId, ulong roleId)
    {
        Invoking(() => new GuildClashOfClansRankRole(_stubDiscordServerId, guildId, ClashOfClansClanRole.Elder))
            .Should().Throw<SystemException>();
    }

    [Fact]
    public void ChangeDiscordRole_ShouldChangeRoleId_WhenAllParametersAreValid()
    {
        // Arrange
        var roleId = 817773544460255262u;

        // Act
        _stubRankRole.ChangeDiscordRole(roleId);

        // Assert
        _stubRankRole.DiscordRoleId.Should().Be(roleId);
    }

    [Fact]
    public void ChangeDiscordRole_ShouldThrowException_WhenInvalidIdProvided()
    {
        Invoking(() => _stubRankRole.ChangeDiscordRole(1))
            .Should().Throw<InvalidOperationException>();
    }

    public static IEnumerable<object[]> InvalidConstructorParameters =>
        new List<object[]>
        {
            new object[] { (ulong)1, 339924145909399562u },
            new object[] { 760910445686161488u, (ulong)1 },
        };
}
