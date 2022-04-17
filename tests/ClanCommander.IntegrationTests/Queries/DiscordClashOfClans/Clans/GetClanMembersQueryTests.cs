﻿using ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Queries.GetClanRoster;

namespace ClanCommander.IntegrationTests.Queries.DiscordClashOfClans.Clans;

public class GetClanRosterQueryTests : TestBase
{
    [Fact]
    public async void ShouldReturnClanRoster_WithDiscordData_WhenClanIsRegisteredToADiscordGuild()
    {
        // Arrange
        var guildMock = new ValidRegisteredDiscordGuildMock();
        await guildMock.SeedToDatabaseAsync(ServiceProvider);

        var userMock = new ValidDiscordUserMock();
        await userMock.SeedToDatabaseAsync(ServiceProvider);

        var accountMock = new ValidClashOfClansAccountMock();
        await accountMock.SeedToDatabaseAsync(ServiceProvider);

        var clanMock = new ValidDiscordClanMock();
        await clanMock.SeedToDatabaseAsync(ServiceProvider);

        // Act
        var result = await Mediator.Send(new GetClanRosterQuery(clanMock.ClanId.Value, clanMock.GuildId.Value));

        // Assert
        result.Should().NotBeNull();
        result.ClanId.Should().Be(clanMock.ClanId.Value);
        result.DiscordGuildId.Should().Be(clanMock.GuildId.Value);
        result.Members.Should().NotBeEmpty();
    }

    [Fact]
    public async void ShouldReturnClanRoster_WhenClanIsNotRegistered()
    {
        // Arrange
        var clanId = "#9UGQ0GL";

        // Act
        var result = await Mediator.Send(new GetClanRosterQuery(clanId, default));

        // Assert
        result.Should().NotBeNull();
        result.ClanId.Should().Be(clanId);
        result.DiscordGuildId.Should().Be(default);
        result.Members.Should().NotBeEmpty();
    }
}
