namespace ClanCommander.ApplicationCore.Services;

internal class ClashOfClansService : IClashOfClansService
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
        var playerTag = string.Concat(id.Where(c => !char.IsWhiteSpace(c))).ToUpper();

        if (!playerTag.StartsWith('#')
            || playerTag.Length < TagMinimumLength
            || playerTag.Length > TagMaximumLength) return null;

        return await _clashOfClans.Players.GetPlayerAsync(playerTag);
    }
}

