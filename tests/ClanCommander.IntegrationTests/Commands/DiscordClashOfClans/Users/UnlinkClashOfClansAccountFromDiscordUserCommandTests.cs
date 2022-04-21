namespace ClanCommander.IntegrationTests.Commands.DiscordClashOfClans.Users;

public class UnlinkClashOfClansAccountFromDiscordUserCommandTests : TestBase
{
    [Fact]
    public async void ShouldUnlinkClashOfClansAccountFromUser()
    {
        // Arrange
        var userMock = new ValidDiscordUserMock();
        await userMock.SeedToDatabaseAsync(ServiceProvider);

        var accountMock = new ValidClashOfClansAccountMock();
        await accountMock.SeedToDatabaseAsync(ServiceProvider);

        // Act
        var result = await Mediator.Send(new UnlinkClashOfClansAccountFromDiscordUserCommand(userMock.UserId.Value, accountMock.AccountId.Value));

        // Assert
        await using var scope = ServiceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var account = await context.ClashOfClansUserAccounts.FirstOrDefaultAsync(x => x.AccountId == accountMock.AccountId && x.UserId == userMock.UserId);

        account.Should().BeNull();
    }

    [Fact]
    public async void ShouldThrowException_WhenAccountHasntBeenLinked()
    {
        // Arrange
        var userMock = new ValidDiscordUserMock();
        await userMock.SeedToDatabaseAsync(ServiceProvider);

        var accountMock = new ValidClashOfClansAccountMock();

        // Act
        // Assert
        await Invoking(async () => await Mediator.Send(new UnlinkClashOfClansAccountFromDiscordUserCommand(userMock.UserId.Value, accountMock.AccountId.Value)))
            .Should().ThrowAsync<ArgumentException>().WithMessage($"There's no account with the id of {accountMock.AccountId.Value} linked to this user");
    }
}
