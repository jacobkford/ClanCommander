namespace ClanCommander.UnitTests.Entities;

public class ClanMemberTests
{
    private readonly string _memberId = "#PQU9QLP2V";
    private readonly ulong _userId = 339924145909399562u;
    private readonly ClanMember _clanMember;

    public ClanMemberTests()
    {
        _clanMember = new ClanMember(_memberId, _userId);
    }

    [Fact]
    public void Constructor_ShouldCreateClanMember_WhenAllParametersAreValid()
    {
        // Arrange
        var memberId = "#PQU9QLP2V";
        var userId = 339924145909399562u;

        // Act
        var result = new ClanMember(memberId, userId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(memberId);
        result.UserId.Should().Be(userId);
        result.CWLParticipant.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(InvalidConstructorParameters))]
    public void Constructor_ShouldThrowException_WhenParameterIsInvalid(string memberId, ulong userId)
    {
        Invoking(() => new ClanMember(memberId, userId))
            .Should().Throw<SystemException>();
    }

    [Fact]
    public void OptInCwl_ShouldSetCWLParticipantToTrue()
    {
        // Arrange
        // Act
        _clanMember.OptInCWL();

        // Assert
        _clanMember.CWLParticipant.Should().BeTrue();
    }

    [Fact]
    public void OptOutCwl_ShouldSetCWLParticipantToFalse()
    {
        // Arrange
        _clanMember.OptInCWL();

        // Act
        _clanMember.OptOutCWL();

        // Assert
        _clanMember.CWLParticipant.Should().BeFalse();
    }

    public static IEnumerable<object[]> InvalidConstructorParameters =>
        new List<object[]>
        {
            new object[] { "", 339924145909399562u },
            new object[] { "#PQU9QLP2V", (ulong)1 }
        };
}
