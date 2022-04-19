using ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Commands.AddClanMemberToGuild;

namespace ClanCommander.IntegrationTests.Commands.DiscordClashOfClans.Clans;

public class AddClanMemberToGuildCommandTests : TestBase
{
    [Fact]
    public async void ShouldRegisterClanMember()
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
        var result = await Mediator.Send(new AddClanMemberToGuildCommand(
            guildMock.GuildId.Value, clanMock.ClanId.Value, accountMock.AccountId.Value, accountMock.UserId.Value));

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(accountMock.AccountId.Value);
    }

    [Fact]
    public async void ShouldThrowException_WhenClanIsNotFound()
    {
        // Arrange
        var invalidClanId = "#0TEST123";

        var guildMock = new ValidRegisteredDiscordGuildMock();
        await guildMock.SeedToDatabaseAsync(ServiceProvider);

        var userMock = new ValidDiscordUserMock();
        await userMock.SeedToDatabaseAsync(ServiceProvider);

        var accountMock = new ValidClashOfClansAccountMock();
        await accountMock.SeedToDatabaseAsync(ServiceProvider);

        await Invoking(async () => 
            await Mediator.Send(new AddClanMemberToGuildCommand(
                guildMock.GuildId.Value, invalidClanId, accountMock.AccountId.Value, accountMock.UserId.Value)))
            .Should().ThrowAsync<ArgumentException>().WithMessage($"Clan with the Id of '{invalidClanId}' was not found.");
    }

    [Fact]
    public async void ShouldThrowException_WhenClanMemberIsNotFound()
    {
        // Arrange
        var invalidMemberId = "#0TEST123";

        var guildMock = new ValidRegisteredDiscordGuildMock();
        await guildMock.SeedToDatabaseAsync(ServiceProvider);

        var userMock = new ValidDiscordUserMock();
        await userMock.SeedToDatabaseAsync(ServiceProvider);

        var accountMock = new ValidClashOfClansAccountMock();
        await accountMock.SeedToDatabaseAsync(ServiceProvider);

        var clanMock = new ValidDiscordClanMock();
        await clanMock.SeedToDatabaseAsync(ServiceProvider);

        await Invoking(async () =>
            await Mediator.Send(new AddClanMemberToGuildCommand(
                guildMock.GuildId.Value, clanMock.ClanId.Value, invalidMemberId, accountMock.UserId.Value)))
            .Should().ThrowAsync<ArgumentException>().WithMessage($"Clan Member with the Id of '{invalidMemberId}' was not found.");
    }

    [Fact]
    public async void ShouldThrowException_WhenClanIsNotRegistered()
    {
        // Arrange
        var guildMock = new ValidRegisteredDiscordGuildMock();
        await guildMock.SeedToDatabaseAsync(ServiceProvider);

        var userMock = new ValidDiscordUserMock();
        await userMock.SeedToDatabaseAsync(ServiceProvider);

        var accountMock = new ValidClashOfClansAccountMock();
        await accountMock.SeedToDatabaseAsync(ServiceProvider);

        var clanMock = new ValidDiscordClanMock();

        await Invoking(async () =>
            await Mediator.Send(new AddClanMemberToGuildCommand(
                guildMock.GuildId.Value, clanMock.ClanId.Value, accountMock.AccountId.Value, accountMock.UserId.Value)))
            .Should().ThrowAsync<ArgumentException>().WithMessage($"{clanMock.ClanName} is not a registered clan in this server.");
    }
}
