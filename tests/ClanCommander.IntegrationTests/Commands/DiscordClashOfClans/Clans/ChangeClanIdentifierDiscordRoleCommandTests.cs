namespace ClanCommander.IntegrationTests.Commands.DiscordClashOfClans.Clans;

public class ChangeClanIdentifierDiscordRoleCommandTests : TestBase
{
    [Fact]
    public async void ShouldChangeClanDiscordRole()
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

        var roleId = 761303410200150017u;

        // Act
        await Mediator.Send(new ChangeClanIdentifierDiscordRoleCommand(guildMock.GuildId.Value, clanMock.ClanId.Value, roleId));

        // Assert
        await using var scope = ServiceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var clan = await context.GuildClans.FirstOrDefaultAsync(x => x.GuildId == clanMock.GuildId && x.ClanId == clanMock.ClanId);

        clan?.DiscordRoleId.Should().Be(roleId);
    }
}
