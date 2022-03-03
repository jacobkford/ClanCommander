using ClanCommander.ApplicationCore.Features.Discord.Users.ClientEvents;

namespace ClanCommander.IntegrationTests.DiscordSocketClientEvents;

public class UserJoinedGuildClientEventTests : TestBase
{
    private readonly DiscordUserId _testUserIdOne = DiscordUserId.FromUInt64(339924145909399562u);
    private readonly string _testUsernameOne = "testuser#0001";
    private readonly DiscordGuildId _testGuildIdOne = DiscordGuildId.FromUInt64(760910445686161488u);
    private readonly string _testGuildNameOne = "Test1";
    private readonly DiscordUserId _testGuildOwnerOne = DiscordUserId.FromUInt64(751887766404726806u);

    public UserJoinedGuildClientEventTests() : base() 
    {
        ApplicationDbContext.DiscordGuilds.Add(new RegisteredDiscordGuild(_testGuildIdOne, _testGuildNameOne, _testGuildOwnerOne));
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async void ShouldCreateUser_WhenUserJoinsGuild_AndHasNotBeenPreviouslyRegistered()
    {
        await Mediator.Publish(new UserJoinedGuildClientEvent(_testUserIdOne.Value, _testUsernameOne, _testGuildIdOne.Value));

        var result = await ApplicationDbContext.DiscordUsers.SingleOrDefaultAsync(x => x.UserId == _testUserIdOne);
        await ApplicationDbContext.Entry(result).ReloadAsync();

        result.Should().NotBeNull();
        result.UserId.Should().Be(_testUserIdOne);
        result.Username.Should().Be(_testUsernameOne);
    }

}
