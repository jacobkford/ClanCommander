using ContextType = Discord.Commands.ContextType;
using SummaryAttribute = Discord.Commands.SummaryAttribute;
using RequireContextAttribute = Discord.Commands.RequireContextAttribute;

namespace ClanCommander.DiscordBot.Modules.ClashOfClans;

public class UsersModule : ModuleBase<SocketCommandContext>
{
    private readonly IMediator _mediator;

    public UsersModule(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Command("link")]
    [Summary("Link a Clash Of Clans Account to a User")]
    public async Task LinkClashOfClansAccountToDiscordUserAsync(string accountId)
    {
        var result = await _mediator.Send(new LinkClashOfClansAccountToDiscordUserCommand(Context.User.Id, accountId))
            ?? throw new InvalidOperationException("There was a problem linking the account, please try again");

        var embed = new SuccessEmbedBuilder()
            .WithTitle("Successfully linked Clash Of Clans account")
            .WithDescription($"{result.AccountName} ({result.AccountId}) has been linked to <@{result.DiscordUserId}>");

        await Context.Channel.SendMessageAsync(embed: embed.Build());
    }

    [Command("link")]
    [Summary("Link a Clash Of Clans Account to a User")]
    [RequireBotOwner]
    public async Task LinkClashOfClansAccountToDiscordUserAsync(SocketUser user, string accountId)
    {
        var result = await _mediator.Send(new LinkClashOfClansAccountToDiscordUserCommand(user.Id, accountId));

        var embed = new SuccessEmbedBuilder()
            .WithTitle("Successfully linked Clash Of Clans account")
            .WithDescription($"{result.AccountName} ({result.AccountId}) has been linked to <@{result.DiscordUserId}>");

        await Context.Channel.SendMessageAsync(embed: embed.Build());
    }

    [Command("me"), Alias("profile", "self")]
    [Summary("Gets all linked Clash Of Clans accounts for the user executing the command")]
    public async Task GetAllUserClashOfClansAccountsAsync()
    {
        var result = await _mediator.Send(new GetAllUserClashOfClansAccountsQuery(Context.User.Id));

        var user = Context.Guild.Users.SingleOrDefault(x => x.Id == result.UserId);
        var accounts = result.ClashOfClansAccounts;

        var embed = new GeneralEmbedBuilder()
            .WithTitle($"{user.Nickname} ({user.Username}#{user.Discriminator})")
            .WithThumbnailUrl(user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl());

        foreach (var account in accounts)
            embed.AddField($"**{account.Name} ({account.Id})**",
                $"TownHall: {account.TownHallLevel}\n" +
                $"Clan: {account.ClanName} - {account.ClanRole}\n");

        await Context.Channel.SendMessageAsync(embed: embed.Build());
    }

    [Command("whois")]
    [Summary("Gets all linked Clash Of Clans accounts for the user executing the command")]
    [RequireContext(ContextType.Guild)]
    public async Task FindGuildUserByClashOfClansAccountAsync(string accountId)
    {
        var result = await _mediator.Send(new FindGuildUserByClashOfClansAccountQuery(Context.Guild.Id, accountId));

        var user = Context.Guild.Users.SingleOrDefault(x => x.Id == result.UserId);
        var account = result.ClashOfClansAccount;

        var embed = new GeneralEmbedBuilder()
            .WithTitle($"{user.Nickname} ({result.Username})")
            .WithThumbnailUrl(user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
            .AddField($"**{account.Name} ({account.Id})**",
                $"TownHall: {account.TownHallLevel}\n" +
                $"Clan: {account.ClanName} - {account.ClanRole}\n");

        await Context.Channel.SendMessageAsync(embed: embed.Build());
    }
}
