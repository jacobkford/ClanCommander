using ClanCommander.ApplicationCore.Entities.Discord.GuildClashOfClans.Clans;
using ClanCommander.ApplicationCore.Entities.Shared;

namespace ClanCommander.UnitTests.Entities.Discord.GuildClashOfClans.Clans;
public class GuildClanMemberTest
{
    private readonly ClashOfClansPlayerId _stubPlayerId = ClashOfClansPlayerId.FromString("#PQU9QLP2V");
    private readonly DiscordUserId _stubDiscordUserId = DiscordUserId.FromUInt64(339924145909399562u);

    [Fact]
    public void Promote_ShouldBeSuccessful()
    {
        // Arrange
        var clanMember = new GuildClanMember(_stubPlayerId, _stubDiscordUserId, ClashOfClansClanRole.Member);

        // Act
        clanMember.Promote();

        // Assert
        clanMember.ClanRole.Should().Be(ClashOfClansClanRole.Elder);
    }

    [Fact]
    public void Promote_ShouldThrowException_WhenItsNotPossibleToPromote()
    {
        // Arrange
        var clanMember = new GuildClanMember(_stubPlayerId, _stubDiscordUserId, ClashOfClansClanRole.Leader);

        // Act
        // Assert
        Invoking(() => clanMember.Promote()).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Demote_ShouldBeSuccessful()
    {
        // Arrange
        var clanMember = new GuildClanMember(_stubPlayerId, _stubDiscordUserId, ClashOfClansClanRole.Elder);

        // Act
        clanMember.Demote();

        // Assert
        clanMember.ClanRole.Should().Be(ClashOfClansClanRole.Member);
    }

    [Fact]
    public void Demote_ShouldThrowException_WhenItsNotPossibleToDemote()
    {
        // Arrange
        var clanMember = new GuildClanMember(_stubPlayerId, _stubDiscordUserId, ClashOfClansClanRole.Member);

        // Act
        // Assert
        Invoking(() => clanMember.Demote()).Should().Throw<InvalidOperationException>();
    }
}
