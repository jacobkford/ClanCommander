namespace ClanCommander.IntegrationTests.Queries;

public class GetDiscordGuildDetailsQueryTests : TestBase
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

    public GetDiscordGuildDetailsQueryTests() : base()
    {
        _testGuildOne = new RegisteredDiscordGuild(_testGuildOneId, _testGuildOneName, _testGuildOneOwner);
        _testGuildCommands = new GuildMessageCommands(_testGuildOneId);
        _testGuildCommands.ChangeMessageCommandPrefix(_testGuildOnePrefix);

        _testGuildTwo = new RegisteredDiscordGuild(_testGuildTwoId, _testGuildTwoName, _testGuildTwoOwner);

        this.SeedTestData();
    }

    [Fact]
    public async void ShouldReturnGuildWithMessageCommandsConfiguration()
    {
        var guildId = 760910445686161488u;

        var result = await Mediator.Send(new GetDiscordGuildDetailsQuery(guildId));

        result.Should().NotBeNull();
        result.Id.Should().Be(_testGuildOneId.Value);
        result.Name.Should().Be(_testGuildOneName);
        result.OwnerId.Should().Be(_testGuildOneOwner.Value);
        result.MessageCommandPrefix.Should().Be(_testGuildOnePrefix);
    }

    [Fact]
    public async void ShouldReturnGuild_WhenMessageCommandsPrefixNotCreated()
    {
        var guildId = 834837883810349098u;

        var result = await Mediator.Send(new GetDiscordGuildDetailsQuery(guildId));

        result.Should().NotBeNull();
        result.Id.Should().Be(_testGuildTwoId.Value);
        result.Name.Should().Be(_testGuildTwoName);
        result.OwnerId.Should().Be(_testGuildTwoOwner.Value);
        result.MessageCommandPrefix.Should().BeNull();
    }

    private void SeedTestData()
    {
        ApplicationDbContext.DiscordGuilds.Add(_testGuildOne);
        ApplicationDbContext.MessageCommandsConfigurations.Add(_testGuildCommands);
        ApplicationDbContext.DiscordGuilds.Add(_testGuildTwo);
        ApplicationDbContext.SaveChanges();
    }
}
