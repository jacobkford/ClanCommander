namespace ClanCommander.ApplicationCore.Features.Discord.Guilds;

public class GuildModule : ModuleBase<SocketCommandContext>
{
    private readonly IMediator _mediator;

    public GuildModule(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Command("guild")]
    public async Task GuildAsync()
    {
        var bot = Context.Client.CurrentUser;

        var data = await _mediator.Send(new GetDiscordGuildDetailsQuery(Context.Guild.Id));

        var embed = new EmbedBuilder();
        embed.WithAuthor($"{bot.Username} - Guild", bot.GetAvatarUrl() ?? bot.GetDefaultAvatarUrl());

        if (data is null)
        {
            embed.AddField("Registered", "❌");
            embed.AddField("Prefix", "!");
        } 
        else
        {
            embed.AddField("Registered", "✅");
            embed.AddField("Prefix", data.MessageCommandPrefix ?? "!");
        }

        embed.AddField("Owner", $"<@{Context.Guild.OwnerId}>");
        embed.AddField("User Count", Context.Guild.Users.Count);

        await Context.Channel.SendMessageAsync(embed: embed.Build());
    }
}
