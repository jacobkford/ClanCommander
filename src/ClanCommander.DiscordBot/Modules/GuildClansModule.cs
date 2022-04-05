using GroupAttribute = Discord.Commands.GroupAttribute;

namespace ClanCommander.DiscordBot.Modules;

public class GuildClansModule : ModuleBase<SocketCommandContext>
{
    [Group("clans")]
    public class ClansModule : ModuleBase<SocketCommandContext>
    {
        private readonly IMediator _mediator;

        public ClansModule(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Command, Alias("get")]
        public async Task GetGuildAsync(string clanId)
        {
            var result = await _mediator.Send(new GetClanDetailsQuery(clanId));

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
    }
}
