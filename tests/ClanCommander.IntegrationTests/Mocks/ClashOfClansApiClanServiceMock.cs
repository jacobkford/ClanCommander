namespace ClanCommander.IntegrationTests.Mocks;

public class ClashOfClansApiClanServiceMock : IClashOfClansApiClanService
{
    public Task<ClashOfClans.Models.Clan?> GetClanAsync(string id)
    {
        if (id is not "#9UGQ0GL")
            return Task.FromResult<ClashOfClans.Models.Clan?>(null);

        return Task.FromResult<ClashOfClans.Models.Clan?>(new ClashOfClans.Models.Clan()
        {
            Tag = "#9UGQ0GL",
            Name = "PlaneClashers",
            Type = ClashOfClans.Models.Type.InviteOnly,
            Description = "Testing",
            ClanPoints = 1000,
            ClanVersusPoints = 1000,
            WarFrequency = ClashOfClans.Models.WarFrequency.Always,
            WarWinStreak = 0,
            WarWins = 100,
            WarLosses = 100,
            WarTies = 1,
            IsWarLogPublic = true,
            WarLeague = new ClashOfClans.Models.WarLeague()
            {
                Id = 48000012,
                Name = "Crystal League I"
            },
            ClanLevel = 25,
            BadgeUrls = new ClashOfClans.Models.UrlContainer() 
            {
                Small = new Uri("https://api-assets.clashofclans.com/badges/70/HzVo_1XdFtLyHj1uyL0VVsxHLW31XnoApkdyArVA1Ok.png"),
                Medium = new Uri("https://api-assets.clashofclans.com/badges/200/HzVo_1XdFtLyHj1uyL0VVsxHLW31XnoApkdyArVA1Ok.png"),
                Large = new Uri("https://api-assets.clashofclans.com/badges/512/HzVo_1XdFtLyHj1uyL0VVsxHLW31XnoApkdyArVA1Ok.png"),
            },
            Members = 1,
            MemberList = new List<ClashOfClans.Models.ClanMember>()
            {
                new ClashOfClans.Models.ClanMember()
                {
                    Tag = "#PQU9QLP2V",
                    Name = "JAY",
                    Role = ClashOfClans.Models.Role.Leader
                }
            },
            Labels = new ClashOfClans.Models.LabelList()
            {
                new ClashOfClans.Models.Label() 
                { 
                    Id = 56000000, 
                    Name = "Clan Wars", 
                    IconUrls = new ClashOfClans.Models.UrlContainer()
                    {
                        Small = new Uri("https://api-assets.clashofclans.com/labels/64/lXaIuoTlfoNOY5fKcQGeT57apz1KFWkN9-raxqIlMbE.png"),
                        Medium = new Uri("https://api-assets.clashofclans.com/labels/128/lXaIuoTlfoNOY5fKcQGeT57apz1KFWkN9-raxqIlMbE.png"),
                    }
                },
                new ClashOfClans.Models.Label()
                {
                    Id = 56000008,
                    Name = "Farming",
                    IconUrls = new ClashOfClans.Models.UrlContainer()
                    {
                        Small = new Uri("https://api-assets.clashofclans.com/labels/64/iLWz6AiaIHg_DqfG6s9vAxUJKb-RsPbSYl_S0ii9GAM.png"),
                        Medium = new Uri("https://api-assets.clashofclans.com/labels/128/iLWz6AiaIHg_DqfG6s9vAxUJKb-RsPbSYl_S0ii9GAM.png"),
                    }
                },
                new ClashOfClans.Models.Label()
                {
                    Id = 56000013,
                    Name = "Relaxed",
                    IconUrls = new ClashOfClans.Models.UrlContainer()
                    {
                        Small = new Uri("https://api-assets.clashofclans.com/labels/64/Kv1MZQfd5A7DLwf1Zw3tOaUiwQHGMwmRpjZqOalu_hI.png"),
                        Medium = new Uri("https://api-assets.clashofclans.com/labels/128/Kv1MZQfd5A7DLwf1Zw3tOaUiwQHGMwmRpjZqOalu_hI.png"),
                    }
                }
            },
            ChatLanguage = new ClashOfClans.Models.Language()
            {
                Id = 75000000,
                Name = "English",
                LanguageCode = "EN",
            },
            RequiredTownhallLevel = 11,
            RequiredVersusTrophies = 0,
        });
    }
}
