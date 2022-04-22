namespace ClanCommander.IntegrationTests.Queries.DiscordClashOfClans.Users;

public class FindGuildUserByClashOfClansAccountQueryTests : TestBase
{
    [Fact]
    public async void ShouldFindGuildUser_WhenClashOfClansAccountIsLinkedAndRegisteredInDiscordGuild()
    {
        // Arrange
        var guildMock = new ValidRegisteredDiscordGuildMock();
        await guildMock.SeedToDatabaseAsync(ServiceProvider);

        var userMock = new ValidDiscordUserMock();
        await userMock.SeedToDatabaseAsync(ServiceProvider);

        var accountMock = new ValidClashOfClansAccountMock();
        await accountMock.SeedToDatabaseAsync(ServiceProvider);

        var clanMock = new ValidDiscordClanMock();
        clanMock.WithValidClanMember();
        await clanMock.SeedToDatabaseAsync(ServiceProvider);

        // Act
        var result = await Mediator.Send(new FindGuildUserByClashOfClansAccountQuery(guildMock.GuildId.Value, accountMock.AccountId.Value));

        // Assert
        result.Should().NotBeNull();
        result.GuildId.Should().Be(guildMock.GuildId.Value);
        result.UserId.Should().Be(userMock.UserId.Value);
        result.ClashOfClansAccount.Should().NotBeNull();
        result.ClashOfClansAccount!.Id.Should().Be(accountMock.AccountId.Value);
        result.ClashOfClansAccount!.ClanId.Should().Be(clanMock.ClanId!.Value);
    }

    [Fact]
    public async void ShouldThrowException_WhenUserIsntLinkedToAccount()
    {
        // Arrange
        var guildMock = new ValidRegisteredDiscordGuildMock();
        await guildMock.SeedToDatabaseAsync(ServiceProvider);

        var userMock = new ValidDiscordUserMock();
        await userMock.SeedToDatabaseAsync(ServiceProvider);

        var accountMock = new ValidClashOfClansAccountMock();

        // Act
        // Assert
        await Invoking(async () => await Mediator.Send(new FindGuildUserByClashOfClansAccountQuery(guildMock.GuildId.Value, accountMock.AccountId.Value)))
            .Should().ThrowAsync<ArgumentException>().WithMessage($"Couldn't find a user linked with an account with the id '{accountMock.AccountId.Value}' in this server");
    }
}
