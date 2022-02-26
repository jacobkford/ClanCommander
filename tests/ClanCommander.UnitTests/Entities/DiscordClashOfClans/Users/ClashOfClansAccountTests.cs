namespace ClanCommander.UnitTests.Entities.DiscordClashOfClans.Users;

public class ClashOfClansAccountTests
{
    private readonly PlayerId _userAccountId = PlayerId.FromString("#PQU9QLP2V");
    private readonly DiscordUserId _discordUserId = DiscordUserId.FromUInt64(339924145909399562u);
    private readonly string _userAccountName = "JAY";
    private readonly ClashOfClansAccount _userAccount;

    public ClashOfClansAccountTests()
    {
        _userAccount = new ClashOfClansAccount(_userAccountId, _discordUserId, _userAccountName);
    }

    [Fact]
    public void Constructor_ShouldCreateUserAccount_WhenAllParametersAreValid()
    {
        // Arrange
        // Act
        var result = new ClashOfClansAccount(_userAccountId, _discordUserId, _userAccountName);

        // Assert
        result.Should().NotBeNull();
        result.AccountId.Should().Be(_userAccountId);
        result.Name.Should().Be(_userAccountName);
    }

    [Theory]
    [MemberData(nameof(InvalidConstructorParameters))]
    public void Constructor_ShouldThrowException_WhenParameterIsInvalid(string userAccountId, ulong userId, string userAccountName)
    {
        Invoking(() => new ClashOfClansAccount(PlayerId.FromString(userAccountId), DiscordUserId.FromUInt64(userId), userAccountName))
            .Should().Throw<SystemException>();
    }

    [Fact]
    public void UpdateName_ShouldChangeAccountName_WhenParameterIsValid()
    {
        // Arrange
        var newAccountName = "TestName";

        // Act
        _userAccount.UpdateName(newAccountName);

        // Assert
        _userAccount.Name.Should().NotBe(_userAccountName);
        _userAccount.Name.Should().Be(newAccountName);
    }

    [Fact]
    public void UpdateName_ShouldThrowException_WhenParameterIsInvalid()
    {
        Invoking(() => _userAccount.UpdateName(""))
            .Should().Throw<SystemException>();
    }

    public static IEnumerable<object[]> InvalidConstructorParameters =>
        new List<object[]>
        {
            new object[] { "", 339924145909399562u, "JAY" },
            new object[] { "JAY", 339924145909399562u, "JAY" },
            new object[] { "#PQU9QLP2V", (ulong)100, "" },
            new object[] { "#PQU9QLP2V", 339924145909399562u, "" }
        };
}
