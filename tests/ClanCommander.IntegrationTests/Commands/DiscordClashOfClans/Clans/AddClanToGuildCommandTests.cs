namespace ClanCommander.IntegrationTests.Commands.DiscordClashOfClans.Clans;

public class AddClanToGuildCommandTests : TestBase
{
    [Fact]
    public async void ShouldAddClanToGuild()
    {
        // Arrange
        var guildMock = new ValidRegisteredDiscordGuildMock();
        await guildMock.SeedToDatabaseAsync(ServiceProvider);

        var userMock = new ValidDiscordUserMock();
        await userMock.SeedToDatabaseAsync(ServiceProvider);

        var accountMock = new ValidClashOfClansAccountMock();
        await accountMock.SeedToDatabaseAsync(ServiceProvider);

        var clanMock = new ValidDiscordClanMock();

        // Act
        var result = await Mediator.Send(new AddClanToGuildCommand(clanMock.GuildId.Value, clanMock.ClanId.Value));

        // Assert
        result.Should().NotBeNull();
        result.GuildId.Should().Be(clanMock.GuildId.Value);
        result.ClanId.Should().Be(clanMock.ClanId.Value);
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

        // Act
        // Assert
        await Invoking(async () =>
            await Mediator.Send(new AddClanToGuildCommand(guildMock.GuildId.Value, invalidClanId)))
            .Should().ThrowAsync<ArgumentException>().WithMessage($"Clan with the Id of '{invalidClanId}' was not found.");
    }

    [Fact]
    public async void ShouldThrowException_WhenClanHasAlreadyBeenRegistered()
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
        // Assert
        await Invoking(async () =>
            await Mediator.Send(new AddClanToGuildCommand(clanMock.GuildId.Value, clanMock.ClanId.Value)))
            .Should().ThrowAsync<ArgumentException>().WithMessage($"{clanMock.ClanName} has already been registered in this server.");
    }
}
