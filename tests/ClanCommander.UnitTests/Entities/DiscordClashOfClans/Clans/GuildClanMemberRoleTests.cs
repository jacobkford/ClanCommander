﻿namespace ClanCommander.UnitTests.Entities.DiscordClashOfClans.Users;

public class GuildClanMemberRoleTests
{
    private readonly DiscordGuildId _stubDiscordServerId = DiscordGuildId.FromUInt64(760910445686161488u);
    private readonly ulong _stubRoleId = 339924145909399562u;
    private readonly GuildClanMemberRole _stubRankRole;

    public GuildClanMemberRoleTests()
    {
        _stubRankRole = new GuildClanMemberRole(_stubDiscordServerId, _stubRoleId, ClanMemberRole.CoLeader);
    }

    [Fact]
    public void Constructor_ShouldCreateRankRole_WhenAllParametersAreValid()
    {
        // Arrange
        // Act
        var role = new GuildClanMemberRole(_stubDiscordServerId, _stubRoleId, ClanMemberRole.CoLeader);

        // Assert
        role.Should().NotBeNull();
        role.GuildId.Should().Be(_stubDiscordServerId);
        role.DiscordRoleId.Should().Be(_stubRoleId);
        role.InGameRole.Should().Be(ClanMemberRole.CoLeader);

    }

    [Theory]
    [MemberData(nameof(InvalidConstructorParameters))]
    public void Constructor_ShouldThrowException_WhenParametersAreInvalid(ulong guildId, ulong roleId)
    {
        Invoking(() => new GuildClanMemberRole(DiscordGuildId.FromUInt64(guildId), roleId, ClanMemberRole.Elder))
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
            .Should().Throw<SystemException>();
    }

    public static IEnumerable<object[]> InvalidConstructorParameters =>
        new List<object[]>
        {
            new object[] { (ulong)1, 339924145909399562u },
            new object[] { 760910445686161488u, (ulong)1 },
        };
}
