namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Users.Queries.GetAllUserClashOfClansAccounts;

public class GetAllUserClashOfClansAccountsDto
{
    public ulong UserId { get; set; }

    public IReadOnlyCollection<ClashOfClansAccountDto>? ClashOfClansAccounts { get; set; }
}
