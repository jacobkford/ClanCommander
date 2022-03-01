using ClanCommander.ApplicationCore.Services;

namespace ClanCommander.IntegrationTests.Services;

public class MessageCommandServiceTests : TestBase
{
    private readonly CacheService _cacheService;
    private readonly MessageCommandService _messageCommandService;

    private readonly DiscordGuildId _testGuildOneId = DiscordGuildId.FromUInt64(760910445686161488u);
    private readonly string _testGuildOnePrefix = "?";

    private readonly DiscordGuildId _testGuildTwoId = DiscordGuildId.FromUInt64(834837883810349098u);

    private readonly GuildMessageCommands _testGuildOneCommands;

    public MessageCommandServiceTests() : base()
    {
        _cacheService = new CacheService(RedisCache);
        _messageCommandService = new MessageCommandService(Configuration, _cacheService);

        _testGuildOneCommands = new GuildMessageCommands(_testGuildOneId);
        _testGuildOneCommands.ChangeMessageCommandPrefix(_testGuildOnePrefix);

        SeedTestData();
    }

    [Fact]
    public async void ShouldReturnDatabaseGuildPrefixValue_WhenDatabaseEntityExists()
    {
        var result = await _messageCommandService.GetGuildPrefixAsync(_testGuildOneId.Value);

        result.Should().Be(_testGuildOnePrefix);
    }

    [Fact]
    public async void ShouldReturnCacheGuildPrefixValue_WhenCacheValueExists()
    {
        await _cacheService.SetAsync($"guildPrefix_{_testGuildOneId.Value}", "$");

        var result = await _messageCommandService.GetGuildPrefixAsync(_testGuildOneId.Value);

        result.Should().Be("$");
    }

    [Fact]
    public async void ShouldReturnDefaultGuildPrefixValue_WhenCacheAndDatabaseValueDoesNotExist()
    {
        var result = await _messageCommandService.GetGuildPrefixAsync(_testGuildTwoId.Value);

        result.Should().Be("!");
    }

    private void SeedTestData()
    {
        ApplicationDbContext.DiscordGuilds.Add(new RegisteredDiscordGuild(_testGuildOneId, "test", DiscordUserId.FromUInt64(339924145909399562u)));
        ApplicationDbContext.MessageCommandsConfigurations.Add(_testGuildOneCommands);
        ApplicationDbContext.SaveChanges();
    }
}
