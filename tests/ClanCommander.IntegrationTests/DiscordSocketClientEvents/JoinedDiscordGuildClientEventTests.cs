using ClanCommander.ApplicationCore.Features.Discord.Guilds.ClientEvents;

namespace ClanCommander.IntegrationTests.DiscordSocketClientEvents;

public class JoinedDiscordGuildClientEventTests : TestBase
{
    // Test Guild One is to test fully setup guilds
    private readonly DiscordGuildId _testGuildOneId = DiscordGuildId.FromUInt64(760910445686161488u);
    private readonly string _testGuildOneName = "Test1";
    private readonly DiscordUserId _testGuildOneOwner = DiscordUserId.FromUInt64(339924145909399562u);

    // Test Guild Two is to test guilds without message commands setup
    private readonly DiscordGuildId _testGuildTwoId = DiscordGuildId.FromUInt64(834837883810349098u);
    private readonly string _testGuildTwoName = "Test2";
    private readonly DiscordUserId _testGuildTwoOwner = DiscordUserId.FromUInt64(751887766404726806u);
    private readonly RegisteredDiscordGuild _testGuildTwo;

    public JoinedDiscordGuildClientEventTests() : base()
    {
        _testGuildTwo = new RegisteredDiscordGuild(_testGuildTwoId, _testGuildTwoName, _testGuildTwoOwner);

        this.SeedTestData();
    }

    [Fact]
    public async void ShouldRegisterDiscordGuild_WhenBotJoinsGuild()
    {
        await Mediator.Publish(new JoinedDiscordGuildClientEvent(
            _testGuildOneId.Value,
            _testGuildOneName,
            _testGuildOneOwner.Value));

        var result = await ApplicationDbContext.DiscordGuilds.SingleOrDefaultAsync(x => x.GuildId == _testGuildOneId);
        await ApplicationDbContext.Entry(result).ReloadAsync();

        result.Should().NotBeNull();
        result.GuildId.Should().Be(_testGuildOneId);
        result.Name.Should().Be(_testGuildOneName);
        result.OwnerId.Should().Be(_testGuildOneOwner);
    }

    private void SeedTestData()
    {
        ApplicationDbContext.DiscordGuilds.Add(_testGuildTwo);
        ApplicationDbContext.SaveChanges();
    }
}
