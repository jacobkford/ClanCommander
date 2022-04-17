using ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Queries.GetClanRosterComposition;

namespace ClanCommander.IntegrationTests.Queries.DiscordClashOfClans.Clans;

public class GetClanRosterCompositionTests : TestBase
{
    [Fact]
    public async void ShouldReturnRosterComposition()
    {
        // Arrange
        var clanId = "#9UGQ0GL";

        // Act
        var result = await Mediator.Send(new GetClanRosterCompositionQuery(clanId));

        // Assert
        result.Should().NotBeNull();
        result.ClanId.Should().Be(clanId);
        result.HomeVillageComposition.Should().NotBeNullOrEmpty();
    }
}
