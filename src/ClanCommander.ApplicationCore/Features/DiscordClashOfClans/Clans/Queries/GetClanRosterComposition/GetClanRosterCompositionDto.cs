namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Queries.GetClanRosterComposition;

public class GetClanRosterCompositionDto
{
    public string ClanId { get; set; } = default!;

    public string ClanName { get; set; } = default!;

    public string ClanBadgeUrl { get; set; } = default!;

    public IReadOnlyDictionary<int, int>? HomeVillageComposition { get; set; }
}
