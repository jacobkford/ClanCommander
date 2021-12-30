namespace ClanCommander.UnitTests.Entities;

public class UserAccountTests
{
    private readonly string _userAccountId = "#PQU9QLP2V";
    private readonly string _userAccountName = "JAY";
    private readonly UserAccount _userAccount;

    public UserAccountTests()
    {
        _userAccount = new UserAccount(_userAccountId, _userAccountName);
    }

    [Fact]
    public void Constructor_ShouldCreateUserAccount_WhenAllParametersAreValid()
    {
        // Arrange
        var userAccountId = "#PQU9QLP2V";
        var userAccountName = "JAY";

        // Act
        var result = new UserAccount(userAccountId, userAccountName);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(userAccountId);
        result.Name.Should().Be(userAccountName);
    }

    [Theory]
    [MemberData(nameof(InvalidConstructorParameters))]
    public void Constructor_ShouldThrowException_WhenParameterIsInvalid(string userAccountId, string userAccountName)
    {
        Invoking(() => new UserAccount(userAccountId, userAccountName))
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
