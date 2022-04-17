namespace ClanCommander.IntegrationTests.Mocks;

public class ClashOfClansApiPlayerServiceMock : IClashOfClansApiPlayerService
{
    public async Task<ClashOfClans.Models.Player?> GetPlayerAsync(string id)
    {
        if (id is not "#PQU9QLP2V")
            return null;

        return new ClashOfClans.Models.Player
        {
            Tag = "#PQU9QLP2V",
            Name = "JAY",
            TownHallLevel = 14,
            TownHallWeaponLevel = 5,
        };
    }
}
