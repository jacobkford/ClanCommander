namespace ClanCommander.IntegrationTests.Commands.DiscordClashOfClans.Clans;

public class RemoveClanFromGuildCommandTests : TestBase
{
    [Fact]
    public async void ShouldRemoveClanFromGuild()
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
        var result = await Mediator.Send(new RemoveClanFromGuildCommand(clanMock.GuildId.Value, clanMock.ClanId.Value));

        // Assert
        await using var scope = ServiceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var clan = await context.GuildClans.FirstOrDefaultAsync(x => x.GuildId == clanMock.GuildId && x.ClanId == clanMock.ClanId);

        clan.Should().BeNull();
    }
}
