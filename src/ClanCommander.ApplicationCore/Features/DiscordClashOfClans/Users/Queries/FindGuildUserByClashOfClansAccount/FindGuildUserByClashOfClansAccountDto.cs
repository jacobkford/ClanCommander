namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Users.Queries.FindGuildUserByClashOfClansAccount;

public class FindGuildUserByClashOfClansAccountDto
{
    public ulong UserId { get; set; }

    public string Username { get; set; } = default!;

    public ulong GuildId { get; set; }

    public ClashOfClansAccountDto? ClashOfClansAccount { get; set; }
}
