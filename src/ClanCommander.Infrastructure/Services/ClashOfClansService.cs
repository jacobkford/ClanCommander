using ClashOfClans;
using ClashOfClans.Models;

namespace ClanCommander.Infrastructure.Services;

public class ClashOfClansService
{
    // This includes the '#' (example => #FG3)
    private readonly int TagMinimumLength = 4;

    // This includes the '#' (example => #FG3H32RFKK)
    private readonly int TagMaximumLength = 11;

    private readonly IClashOfClans _clashOfClans;

    public ClashOfClansService(IClashOfClans clashOfClans)
    {
        _clashOfClans = clashOfClans;
    }

    public async Task<Player?> FindPlayer(string id)
    {
        var playerTag = String.Concat(id.Where(c => !Char.IsWhiteSpace(c))).ToUpper();

        if (!playerTag.StartsWith('#')
            || playerTag.Length < TagMinimumLength
            || playerTag.Length > TagMaximumLength) return null;

        return await _clashOfClans.Players.GetPlayerAsync(playerTag);
    }
}

