namespace ClanCommander.IntegrationTests.Queries.DiscordClashOfClans.Clans;

public class GetClanDetailsQueryTests : TestBase
{
    public GetClanDetailsQueryTests() 
        : base() { }

    [Fact]
    public async void ShouldReturnClanDetails()
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
        var result = await Mediator.Send(new GetClanDetailsQuery(clanMock.ClanId.Value));

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(clanMock.ClanId.Value);
    }
}
