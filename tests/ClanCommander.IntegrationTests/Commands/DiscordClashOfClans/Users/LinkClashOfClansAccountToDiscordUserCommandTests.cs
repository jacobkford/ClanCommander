namespace ClanCommander.IntegrationTests.Commands.DiscordClashOfClans.Users;

public class LinkClashOfClansAccountToDiscordUserCommandTests : TestBase
{
    [Fact]
    public async void ShouldLinkClashOfClansAccountToUser()
    {
        // Arrange
        var mock = new ValidDiscordUserMock();
        await mock.SeedToDatabaseAsync(ServiceProvider);

        await using var scope = ServiceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var user = await context.DiscordUsers.FirstOrDefaultAsync(x => x.UserId == mock.UserId);

        // Act
        var result = await Mediator.Send(new LinkClashOfClansAccountToDiscordUserCommand(user.UserId.Value, "#PQU9QLP2V"));

        // Assert
        result.Should().NotBeNull();
        result.AccountId.Should().Be("#PQU9QLP2V");
        result.AccountName.Should().Be("JAY");
        result.DiscordUserId.Should().Be(mock.UserId.Value);
        result.DiscordUsername.Should().Be(mock.UserName);
    }

    [Fact]
    public async void ShouldLinkClashOfClansAccount_AndCreateDiscordUser_WhenUserHasntBeenPreviouslyRegistered()
    {
        // Arrange
        var userId = 339924145909399562u;
        var accountId = "#PQU9QLP2V";

        // Act
        var result = await Mediator.Send(new LinkClashOfClansAccountToDiscordUserCommand(userId, accountId));

        // Assert
        result.Should().NotBeNull();
        result.AccountId.Should().Be(accountId);
        result.AccountName.Should().Be("JAY");
        result.DiscordUserId.Should().Be(userId);
        result.DiscordUsername.Should().Be("TestUser#0001");
    }

    [Fact]
    public async void ShouldThrowException_WhenClashOfClansAccountDoesntExist()
    {
        // Arrange
        var userId = 339924145909399562u;
        var accountId = "#91T3E1ST";

        // Act
        // Assert
        await Invoking(async () => await Mediator.Send(new LinkClashOfClansAccountToDiscordUserCommand(userId, accountId)))
            .Should().ThrowAsync<ArgumentException>().WithMessage($"Couldn't find an account with the id of '{accountId}'");
    }

    [Fact]
    public async void ShouldThrowException_WhenDiscordUserDoesntExist()
    {
        // Arrange
        var userId = 267644451860512771u;
        var accountId = "#PQU9QLP2V";

        // Act
        // Assert
        await Invoking(async () => await Mediator.Send(new LinkClashOfClansAccountToDiscordUserCommand(userId, accountId)))
            .Should().ThrowAsync<ArgumentException>().WithMessage($"Couldn't find the discord user");
    }

    [Fact]
    public async void ShouldThrowException_WhenClashOfClansAccountIsAlreadyLinkedToUser()
    {
        // Arrange
        var userMock = new ValidDiscordUserMock();
        await userMock.SeedToDatabaseAsync(ServiceProvider);

        var accountMock = new ValidClashOfClansAccountMock();
        await accountMock.SeedToDatabaseAsync(ServiceProvider);

        // Act
        // Assert
        await Invoking(async () => await Mediator.Send(new LinkClashOfClansAccountToDiscordUserCommand(userMock.UserId.Value, accountMock.AccountId.Value)))
            .Should().ThrowAsync<ArgumentException>().WithMessage("Account has already been linked to this user");
    }
}
