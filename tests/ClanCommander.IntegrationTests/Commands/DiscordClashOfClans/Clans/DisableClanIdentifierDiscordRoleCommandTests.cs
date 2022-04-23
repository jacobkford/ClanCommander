namespace ClanCommander.IntegrationTests.Commands.DiscordClashOfClans.Clans;

public class DisableClanIdentifierDiscordRoleCommandTests : TestBase
{
    [Fact]
    public async void ShouldDisableClanDiscordRole()
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
        await Mediator.Send(new DisableClanIdentifierDiscordRoleCommand(guildMock.GuildId.Value, clanMock.ClanId.Value));

        // Assert
        await using var scope = ServiceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var clan = await context.GuildClans.FirstOrDefaultAsync(x => x.GuildId == clanMock.GuildId && x.ClanId == clanMock.ClanId);

        clan?.DiscordRoleId.Should().Be(default);
    }
}
