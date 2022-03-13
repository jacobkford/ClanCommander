namespace ClanCommander.IntegrationTests.Services;

public class MessageCommandServiceTests : TestBase
{
    private readonly CacheService _cacheService;
    private readonly MessageCommandService _messageCommandService;

    public MessageCommandServiceTests() 
        : base()
    {
        _cacheService = new CacheService(RedisCache);
        _messageCommandService = new MessageCommandService(Configuration, _cacheService);
    }

    [Fact]
    public async void ShouldReturnDatabaseGuildPrefixValue_WhenDatabaseEntityExists()
    {
        // Arrange
        var mock = new ValidRegisteredDiscordGuildMock();
        await mock.SeedToDatabaseAsync(ServiceProvider);

        // Act
        var result = await _messageCommandService.GetGuildPrefixAsync(mock.GuildId.Value);

        // Assert
        result.Should().Be(mock.GuildPrefix);
    }

    [Fact]
    public async void ShouldReturnCacheGuildPrefixValue_WhenCacheValueExists()
    {
        // Arrange
        var guildId = 834837883810349098u;
        await _cacheService.SetAsync($"discord:guild:{guildId}:prefix", "$");

        // Act
        var result = await _messageCommandService.GetGuildPrefixAsync(guildId);

        // Assert
        result.Should().Be("$");
    }

    [Fact]
    public async void ShouldReturnDefaultGuildPrefixValue_WhenCacheAndDatabaseValueDoesNotExist()
    {
        // Arrange
        var guildId = 870307653321117746u;

        // Act
        var result = await _messageCommandService.GetGuildPrefixAsync(guildId);

        // Assert
        result.Should().Be(GuildMessageCommands.DefaultMessageCommandPrefix);
    }
}
