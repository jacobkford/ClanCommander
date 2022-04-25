using ContextType = Discord.Commands.ContextType;
using GroupAttribute = Discord.Commands.GroupAttribute;
using SummaryAttribute = Discord.Commands.SummaryAttribute;
using RequireContextAttribute = Discord.Commands.RequireContextAttribute;
using RequireUserPermissionAttribute = Discord.Commands.RequireUserPermissionAttribute;

namespace ClanCommander.DiscordBot.Modules;

[Group("clans")]
public class ClansModule : ModuleBase<SocketCommandContext>
{
    private readonly IMediator _mediator;

    public ClansModule(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Command, Alias("get", "find")]
    [Summary("Finds a Clash Of Clans's clan and returns back information about the clan")]
    public async Task GetGuildAsync(string clanId)
    {
        var result = await _mediator.Send(new GetClanDetailsQuery(clanId, Context.Guild.Id));

        var embed = new GeneralEmbedBuilder()
            .WithTitle($"{result.Name} ({result.Id})")
            .WithThumbnailUrl(result.ClanBadgeUrl);

        if (result.Registered)
        {
            embed.WithDescription("*This clan is registered*");
            embed.AddField("**🆔 Guild Id**", result.DiscordGuildId, true);
            embed.AddField("**🎖 Guild Role**", $"<@&{result.DiscordRoleId}>", true);
            embed.AddField("**👑 Leader**", $"<@{result.LeaderDiscordUserId}> ({result.LeaderName})", true);
            embed.AddField("**📈 Clan Members**", result.MemberCount, true);
            embed.AddField("**📉 Guild Members**", result.RegisteredMemberCount, true);
        }
        else
        {
            embed.WithDescription("*This clan isn't registered*");
            embed.AddField("**👑 Leader**", $"{result.LeaderName}", true);
            embed.AddField("**📈 Clan Members**", result.MemberCount, true);
        }

        await Context.Channel.SendMessageAsync(embed: embed.Build());
    }

    [Command("add"), Alias("post", "register")]
    [Summary("Adds a clan to the discord guild")]
    [RequireContext(ContextType.Guild)]
    [RequireUserPermission(GuildPermission.Administrator)]
    public async Task AddGuildClanAsync(string clanId)
    {
        await _mediator.Send(new AddClanToGuildCommand(Context.Guild.Id, clanId));

        var embed = new SuccessEmbedBuilder()
            .WithTitle($"Successfully added '{clanId}' to this server");

        await Context.Channel.SendMessageAsync(embed: embed.Build());
    }

    [Command("remove"), Alias("delete")]
    [Summary("Removes a clan from the discord guild")]
    [RequireContext(ContextType.Guild)]
    [RequireUserPermission(GuildPermission.Administrator)]
    public async Task RemoveGuildClanAsync(string clanId)
    {
        await _mediator.Send(new RemoveClanFromGuildCommand(Context.Guild.Id, clanId));

        var embed = new SuccessEmbedBuilder()
            .WithTitle($"Successfully removed '{clanId}' from this server");

        await Context.Channel.SendMessageAsync(embed: embed.Build());
    }

    [Group("members"), Alias("roster")]
    public class RosterModule : ModuleBase<SocketCommandContext>
    {
        private readonly IMediator _mediator;

        public RosterModule(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Command, Alias("get")]
        [Summary("Returns the clan roster for the specified clan")]
        public async Task GetRosterAsync(string clanId)
        {
            var result = await _mediator.Send(new GetClanMembersQuery(clanId, Context.Guild.Id));

            if (result is null || result.Members is null)
                throw new InvalidOperationException("Couldn't get clan data");

            var embed = new GeneralEmbedBuilder()
                .WithTitle($"{result.ClanName} Clan Roster");

            var rosterString = new StringBuilder();

            foreach(var member in result.Members)
                rosterString.AppendLine($"{member.ExpLevel} {member.Name} {member.ClanRole}");
            
            embed.Description = rosterString.ToString();

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }

        [Command("add"), Alias("post", "register")]
        [Summary("Adds a user with their account to the clan roster")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task AddUserToClanRoster(SocketGuildUser user, string memberId, string clanId)
        {
            await _mediator.Send(new AddClanMemberToGuildCommand(Context.Guild.Id, clanId, memberId, user.Id));

            var embed = new SuccessEmbedBuilder()
                .WithTitle($"Successfully added '{memberId}' to '{clanId}'");

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }

        [Command("remove"), Alias("delete")]
        [Summary("Removes a user with their account from the clan roster")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task RemoveUserToClanRoster(string memberId, string clanId)
        {
            await _mediator.Send(new RemoveGuildClanMemberCommand(Context.Guild.Id, clanId, memberId));

            var embed = new SuccessEmbedBuilder()
                .WithTitle($"Successfully removed '{memberId}' from '{clanId}'");

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }

        [Command("composition"), Alias("comp", "compo")]
        [Summary("Returns the clan roster composition for the specified clan")]
        public async Task GetRosterComposition(string clanId)
        {
            var result = await _mediator.Send(new GetClanRosterCompositionQuery(clanId));

            if (result is null || result.HomeVillageComposition is null)
                throw new InvalidOperationException("Couldn't get clan roster data");

            var embed = new GeneralEmbedBuilder()
                .WithTitle($"{result.ClanName} ({result.ClanId})")
                .WithThumbnailUrl(result.ClanBadgeUrl);

            var compString = new StringBuilder();

            foreach (var townHallData in result.HomeVillageComposition)
                compString.AppendLine($"**Town Hall {townHallData.Key}'s** : *{townHallData.Value}*");
            
            embed.Description = compString.ToString();

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}

