using ClashOfClans;

namespace ClanCommander.ApplicationCore.Services;

internal class ClashOfClansApiPlayerService : IClashOfClansApiPlayerService
{
    private readonly ClashOfClansClient _api;

    public ClashOfClansApiPlayerService(IConfiguration configuration)
    {
        _api = new ClashOfClansClient(configuration["ClashOfClansAPI:Token"]);
    }

    public async Task<ClashOfClans.Models.Player?> GetPlayerAsync(string id)
    {
        try
        {
            var data = await _api.Players.GetPlayerAsync(id);
            return data;
        }
        catch (ClashOfClans.Core.ClashOfClansException)
        {
            return null;
        }
    }
}
