namespace ClanCommander.UnitTests.Entities.Discord.Users;

public class DiscordUserTests
{
    private readonly ulong _userId = 339924145909399562u;
    private readonly string _userDiscordUsername = "Jaycub#2554";
    private readonly DiscordUser _user;

    public DiscordUserTests()
    {
        _user = new DiscordUser(DiscordUserId.FromUInt64(_userId), _userDiscordUsername);
    }

    [Fact]
    public void Constructor_ShouldCreateUser_WhenAllParametersAreValid()
    {
        // Arrange
        var userId = 339924145909399562u;
        var userDiscordUsername = "Jaycub#2554";

        // Act
        var result = new DiscordUser(DiscordUserId.FromUInt64(userId), userDiscordUsername);

        // Assert
        result.Should().NotBeNull();
        result.UserId.Value.Should().Be(userId);
        result.Username.Should().Be(userDiscordUsername);
    }

    [Theory]
    [MemberData(nameof(InvalidConstructorParameters))]
    public void Constructor_ShouldThrowException_WhenParameterIsInvalid(ulong userId, string userDiscordUsername)
    {
        Invoking(() => new DiscordUser(DiscordUserId.FromUInt64(userId), userDiscordUsername))
            .Should().Throw<SystemException>();
    }

    public static IEnumerable<object[]> InvalidConstructorParameters =>
        new List<object[]>
        {
            new object[] { (ulong)1, "Jaycub#2554" },
            new object[] { 339924145909399562u, "" },
        };
}
