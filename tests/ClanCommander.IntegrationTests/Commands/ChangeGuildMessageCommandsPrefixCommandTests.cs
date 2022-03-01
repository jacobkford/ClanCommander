using ClanCommander.ApplicationCore.Features.Discord.Guilds.Commands.ChangeGuildMessageCommandsPrefix;

namespace ClanCommander.IntegrationTests.Commands;

public class ChangeGuildMessageCommandsPrefixCommandTests : TestBase
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

    public ChangeGuildMessageCommandsPrefixCommandTests() : base()
    {
        _testGuildOne = new RegisteredDiscordGuild(_testGuildOneId, _testGuildOneName, _testGuildOneOwner);
        _testGuildCommands = new GuildMessageCommands(_testGuildOneId);
        _testGuildCommands.ChangeMessageCommandPrefix(_testGuildOnePrefix);

        _testGuildTwo = new RegisteredDiscordGuild(_testGuildTwoId, _testGuildTwoName, _testGuildTwoOwner);

        this.SeedTestData();
    }

    [Fact]
    public async void ShouldChangeGuildMessageCommandsPrefix_WhenPrefixHasPreviouslyBeenSetup()
    {
        var newPrefix = "$";

        var result = await Mediator.Send(new ChangeGuildMessageCommandsPrefixCommand(_testGuildOneId.Value, newPrefix));

        result.Should().NotBeNull();
        result.GuildId.Should().Be(_testGuildOneId.Value);
        result.OldPrefix.Should().Be(_testGuildOnePrefix);
        result.NewPrefix.Should().Be(newPrefix);
    }

    [Fact]
    public async void ShouldCreateGuildMessageCommandAndChangePrefix_WhenGuildMessageCommandEntityDoesNotExist()
    {
        var newPrefix = "£";

        var result = await Mediator.Send(new ChangeGuildMessageCommandsPrefixCommand(_testGuildTwoId.Value, newPrefix));

        result.Should().NotBeNull();
        result.GuildId.Should().Be(_testGuildTwoId.Value);
        result.OldPrefix.Should().Be(GuildMessageCommands.DefaultMessageCommandPrefix);
        result.NewPrefix.Should().Be(newPrefix);
    }

    private void SeedTestData()
    {
        ApplicationDbContext.DiscordGuilds.Add(_testGuildOne);
        ApplicationDbContext.MessageCommandsConfigurations.Add(_testGuildCommands);
        ApplicationDbContext.DiscordGuilds.Add(_testGuildTwo);
        ApplicationDbContext.SaveChanges();
    }
}
