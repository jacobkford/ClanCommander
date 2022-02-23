using ClanCommander.ApplicationCore.Entities.Discord.Users;
using ClanCommander.ApplicationCore.Entities.Shared;

namespace ClanCommander.UnitTests.Entities.Discord.Users;

public class UserAccountTests
{
    private readonly ClashOfClansPlayerId _userAccountId = ClashOfClansPlayerId.FromString("#PQU9QLP2V");
    private readonly string _userAccountName = "JAY";
    private readonly UserClashOfClansAccount _userAccount;

    public UserAccountTests()
    {
        _userAccount = new UserClashOfClansAccount(_userAccountId, _userAccountName);
    }

    [Fact]
    public void Constructor_ShouldCreateUserAccount_WhenAllParametersAreValid()
    {
        // Arrange
        // Act
        var result = new UserClashOfClansAccount(_userAccountId, _userAccountName);

        // Assert
        result.Should().NotBeNull();
        result.AccountId.Should().Be(_userAccountId);
        result.Name.Should().Be(_userAccountName);
    }

    [Theory]
    [MemberData(nameof(InvalidConstructorParameters))]
    public void Constructor_ShouldThrowException_WhenParameterIsInvalid(string userAccountId, string userAccountName)
    {
        Invoking(() => new UserClashOfClansAccount(ClashOfClansPlayerId.FromString(userAccountId), userAccountName))
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
            new object[] { "", "JAY" },
            new object[] { "JAY", "JAY" },
            new object[] { "#PQU9QLP2V", "" }
        };
}
