using ClanCommander.ApplicationCore.Features.Discord.Guilds.ClientEvents;

namespace ClanCommander.IntegrationTests.DiscordSocketClientEvents;

public class DiscordGuildUpdatedClientEventTests : TestBase
{
    // Test Guild One is to test fully setup guilds
    private readonly DiscordGuildId _testGuildOneId = DiscordGuildId.FromUInt64(760910445686161488u);
    private readonly string _testGuildOneName = "Test1";
    private readonly DiscordUserId _testGuildOneOwner = DiscordUserId.FromUInt64(339924145909399562u);
    private readonly string _testGuildOnePrefix = "?";
    private readonly RegisteredDiscordGuild _testGuildOne;
    private readonly GuildMessageCommands _testGuildCommands;

    // Test Guild Two is to test guilds without message commands setup
    private readonly DiscordGuildId _testGuildTwoId = DiscordGuildId.FromUInt64(834837883810349098u);
    private readonly string _testGuildTwoName = "Test2";
    private readonly DiscordUserId _testGuildTwoOwner = DiscordUserId.FromUInt64(751887766404726806u);
    private readonly RegisteredDiscordGuild _testGuildTwo;

    public DiscordGuildUpdatedClientEventTests() : base()
    {
        _testGuildOne = new RegisteredDiscordGuild(_testGuildOneId, _testGuildOneName, _testGuildOneOwner);
        _testGuildCommands = new GuildMessageCommands(_testGuildOneId);
        _testGuildCommands.ChangeMessageCommandPrefix(_testGuildOnePrefix);

        _testGuildTwo = new RegisteredDiscordGuild(_testGuildTwoId, _testGuildTwoName, _testGuildTwoOwner);

        this.SeedTestData();
    }

    [Fact]
    public async void ShouldUpdateRegisteredGuild_WhenChangesAreMade()
    {
        var newName = "Test123";
        var newOwner = 751887766404726806u;

        await Mediator.Publish(new DiscordGuildUpdatedClientEvent(
            _testGuildOneId.Value,
            _testGuildOneName,
            newName,
            _testGuildOneOwner.Value,
            newOwner));

        var result = await ApplicationDbContext.DiscordGuilds.SingleOrDefaultAsync(x => x.GuildId == _testGuildOneId);
        await ApplicationDbContext.Entry(result).ReloadAsync();

        result.Should().NotBeNull();
        result.GuildId.Should().Be(_testGuildOneId);
        result.Name.Should().Be(newName);
        result.OwnerId.Should().Be(DiscordUserId.FromUInt64(newOwner));
    }

    [Fact]
    public async void ShouldCreateAndUpdateRegisteredGuild_WhenChangesAreMade_AndGuildHasNotBeenRegistered()
    {
        var newName = "Test321";
        var newOwner = 339924145909399562u;

        await Mediator.Publish(new DiscordGuildUpdatedClientEvent(
            _testGuildTwoId.Value,
            _testGuildTwoName,
            newName,
            _testGuildTwoOwner.Value,
            newOwner));

        var result = await ApplicationDbContext.DiscordGuilds.SingleOrDefaultAsync(x => x.GuildId == _testGuildTwoId);
        await ApplicationDbContext.Entry(result).ReloadAsync();

        result.Should().NotBeNull();
        result.GuildId.Should().Be(_testGuildTwoId);
        result.Name.Should().Be(newName);
        result.OwnerId.Should().Be(DiscordUserId.FromUInt64(newOwner));
    }

    private void SeedTestData()
    {
        ApplicationDbContext.DiscordGuilds.Add(_testGuildOne);
        ApplicationDbContext.MessageCommandsConfigurations.Add(_testGuildCommands);
        ApplicationDbContext.DiscordGuilds.Add(_testGuildTwo);
        ApplicationDbContext.SaveChanges();
    }
}
