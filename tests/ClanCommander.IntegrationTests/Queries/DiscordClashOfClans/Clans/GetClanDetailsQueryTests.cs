namespace ClanCommander.IntegrationTests.Queries.DiscordClashOfClans.Clans;

public class GetClanDetailsQueryTests : TestBase
{
    [Fact]
    public async void ShouldReturnClanDetails_WithDiscordData_WhenClanIsRegisteredToDiscordGuild()
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
        var result = await Mediator.Send(new GetClanDetailsQuery(clanMock.ClanId.Value, clanMock.GuildId.Value));

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(clanMock.ClanId.Value);
        result.DiscordGuildId.Should().Be(clanMock.GuildId.Value);
    }

    [Fact]
    public async void ShouldReturnClanDetails_WhenClanIsNotRegistered()
    {
        // Arrange
        var clanId = "#9UGQ0GL";

        // Act
        var result = await Mediator.Send(new GetClanDetailsQuery(clanId, default));

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(clanId);
        result.DiscordGuildId.Should().Be(default);
    }
}
