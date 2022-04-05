using ClashOfClans;

namespace ClanCommander.ApplicationCore.Services;

internal class ClashOfClansApiClanService : IClashOfClansApiClanService
{
    private readonly ClashOfClansClient _api;

    public ClashOfClansApiClanService(IConfiguration configuration)
    {
        _api = new ClashOfClansClient(configuration["ClashOfClansAPI:Token"]);
    }

    public async Task<ClashOfClans.Models.Clan?> GetClanAsync(string id)
    {
        try
        {
            var data = await _api.Clans.GetClanAsync(id);
            return data;
        }
        catch (ClashOfClans.Core.ClashOfClansException)
        {
            return null;
        }
    }
}
