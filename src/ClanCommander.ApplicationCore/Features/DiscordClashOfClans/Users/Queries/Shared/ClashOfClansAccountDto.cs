namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Users.Queries.Shared;

public class ClashOfClansAccountDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? ClanId { get; set; }
    public string? ClanName { get; set; }
    public string? ClanRole { get; set; }
    public int TownHallLevel { get; set; }
    public int ExpLevel { get; set; }
    public IReadOnlyDictionary<string, int>? HeroLevels { get; set; }

}
