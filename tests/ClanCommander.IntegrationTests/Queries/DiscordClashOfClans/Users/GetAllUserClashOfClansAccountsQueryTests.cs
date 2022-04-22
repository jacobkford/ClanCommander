namespace ClanCommander.IntegrationTests.Queries.DiscordClashOfClans.Users;

public class GetAllUserClashOfClansAccountsQueryTests : TestBase
{
    [Fact]
    public async void ShouldGetAllUserAccounts()
    {
        // Arrange
        var guildMock = new ValidRegisteredDiscordGuildMock();
        await guildMock.SeedToDatabaseAsync(ServiceProvider);

        var userMock = new ValidDiscordUserMock();
        await userMock.SeedToDatabaseAsync(ServiceProvider);

        var accountMock = new ValidClashOfClansAccountMock();
        await accountMock.SeedToDatabaseAsync(ServiceProvider);

        // Act
        var result = await Mediator.Send(new GetAllUserClashOfClansAccountsQuery(userMock.UserId.Value));

        // Arrange
        result.Should().NotBeNull();
        result.UserId.Should().Be(userMock.UserId.Value);
        result.ClashOfClansAccounts.Should().HaveCount(1);
    }
}
