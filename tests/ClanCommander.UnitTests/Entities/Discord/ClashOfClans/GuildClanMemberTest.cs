namespace ClanCommander.UnitTests.Entities.Discord.ClashOfClans;

public class GuildClanMemberTest
{
    private readonly PlayerId _stubPlayerId = PlayerId.FromString("#PQU9QLP2V");
    private readonly DiscordUserId _stubDiscordUserId = DiscordUserId.FromUInt64(339924145909399562u);

    [Fact]
    public void Promote_ShouldBeSuccessful()
    {
        // Arrange
        var clanMember = new GuildClanMember(_stubPlayerId, _stubDiscordUserId, ClanMemberRole.Member);

        // Act
        clanMember.Promote();

        // Assert
        clanMember.ClanRole.Should().Be(ClanMemberRole.Elder);
    }

    [Fact]
    public void Promote_ShouldThrowException_WhenItsNotPossibleToPromote()
    {
        // Arrange
        var clanMember = new GuildClanMember(_stubPlayerId, _stubDiscordUserId, ClanMemberRole.Leader);

        // Act
        // Assert
        Invoking(() => clanMember.Promote()).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Demote_ShouldBeSuccessful()
    {
        // Arrange
        var clanMember = new GuildClanMember(_stubPlayerId, _stubDiscordUserId, ClanMemberRole.Elder);

        // Act
        clanMember.Demote();

        // Assert
        clanMember.ClanRole.Should().Be(ClanMemberRole.Member);
    }

    [Fact]
    public void Demote_ShouldThrowException_WhenItsNotPossibleToDemote()
    {
        // Arrange
        var clanMember = new GuildClanMember(_stubPlayerId, _stubDiscordUserId, ClanMemberRole.Member);

        // Act
        // Assert
        Invoking(() => clanMember.Demote()).Should().Throw<InvalidOperationException>();
    }
}
