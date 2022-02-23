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
        result.Accounts.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(InvalidConstructorParameters))]
    public void Constructor_ShouldThrowException_WhenParameterIsInvalid(ulong userId, string userDiscordUsername)
    {
        Invoking(() => new DiscordUser(DiscordUserId.FromUInt64(userId), userDiscordUsername))
            .Should().Throw<SystemException>();
    }

    [Fact]
    public void AddAccount_ShouldAddAccountToList_WhenAllParametersAreValid()
    {
        // Arrange
        var accountId = PlayerId.FromString("#PQU9QLP2V");
        var accountName = "JAY";

        // Act
        _user.AddAccount(accountId, accountName);

        // Assert
        _user.Accounts.Should().NotBeEmpty()
            .And.ContainSingle(userAccount => 
                userAccount.AccountId == accountId && userAccount.Name == accountName);
    }

    [Fact]
    public void AddAccount_ShouldThrowException_WhenAddingDuplicateAccount()
    {
        // Arrange
        var accountId = PlayerId.FromString("#PQU9QLP2V");
        var accountName = "JAY";
        _user.AddAccount(accountId, accountName);

        // Act
        // Assert
        Invoking(() => _user.AddAccount(accountId, accountName))
            .Should().Throw<ArgumentException>();
    }

    public static IEnumerable<object[]> InvalidConstructorParameters =>
        new List<object[]>
        {
            new object[] { (ulong)1, "Jaycub#2554" },
            new object[] { 339924145909399562u, "" },
        };
}
