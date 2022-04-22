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
            Role = ClashOfClans.Models.Role.Leader,
            ExpLevel = 212,
            Clan = new ClashOfClans.Models.PlayerClan
            {
                Tag = "#9UGQ0GL",
                Name = "PlaneClashers",
            },
            TownHallLevel = 14,
            TownHallWeaponLevel = 5,
            Heroes = new ClashOfClans.Models.PlayerItemLevelList
            {
                new ClashOfClans.Models.PlayerItemLevel
                {
                    Name = "Barbarian King",
                    Level = 80,
                    MaxLevel = 80,
                    Village = ClashOfClans.Models.Village.Home,
                },
                new ClashOfClans.Models.PlayerItemLevel
                {
                    Name = "Archer Queen",
                    Level = 80,
                    MaxLevel = 80,
                    Village = ClashOfClans.Models.Village.Home,
                },
                new ClashOfClans.Models.PlayerItemLevel
                {
                    Name = "Garden Warden",
                    Level = 55,
                    MaxLevel = 55,
                    Village = ClashOfClans.Models.Village.Home,
                },
                new ClashOfClans.Models.PlayerItemLevel
                {
                    Name = "Royal Champion",
                    Level = 30,
                    MaxLevel = 30,
                    Village = ClashOfClans.Models.Village.Home,
                },
                new ClashOfClans.Models.PlayerItemLevel
                {
                    Name = "Master Builder",
                    Level = 30,
                    MaxLevel = 30,
                    Village = ClashOfClans.Models.Village.Home,
                },
            }
        };
    }
}
