namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Users.Commands.LinkClashOfClansAccountToDiscordUser;

public class LinkClashOfClansAccountToDiscordUserDto
{
    public ulong DiscordUserId { get; set; }

    public string DiscordUsername { get; set; } = default!;

    public string AccountId { get; set; } = default!;

    public string AccountName { get; set; } = default!;
}
