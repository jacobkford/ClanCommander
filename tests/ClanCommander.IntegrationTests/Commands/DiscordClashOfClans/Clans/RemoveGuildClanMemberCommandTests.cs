namespace ClanCommander.IntegrationTests.Commands.DiscordClashOfClans.Clans;

public class RemoveGuildClanMemberCommandTests : TestBase
{
    [Fact]
    public async void ShouldRemoveClanMember_FromClanMemberList()
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
        var result = await Mediator.Send(new RemoveGuildClanMemberCommand(clanMock.GuildId.Value, clanMock.ClanId.Value, clanMock.LeaderId.Value));

        // Assert
        await using var scope = ServiceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var clan = await context.GuildClans.FirstOrDefaultAsync(x => x.GuildId == clanMock.GuildId && x.ClanId == clanMock.ClanId);

        clan.Should().NotBeNull();
        clan!.Members.Should().BeEmpty();
    }
}
