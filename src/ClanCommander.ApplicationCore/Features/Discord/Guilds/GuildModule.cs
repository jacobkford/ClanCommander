using ClanCommander.ApplicationCore.Features.Discord.Guilds.Commands.ChangeGuildMessageCommandsPrefix;

namespace ClanCommander.ApplicationCore.Features.Discord.Guilds;

[Group("guild"), Alias("server")]
[RequireContext(ContextType.Guild)]
public class GuildModule : ModuleBase<SocketCommandContext>
{
    private readonly IMediator _mediator;

    public GuildModule(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Command, Alias("info")]
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

    [Group("prefix")]
    public class GuildPrefixModule: ModuleBase<SocketCommandContext>
    {
        private readonly IMediator _mediator;
        private readonly IMessageCommandService _messageCommandService;

        public GuildPrefixModule(IMediator mediator, IMessageCommandService messageCommandService)
        {
            _mediator = mediator;
            _messageCommandService = messageCommandService;
        }

        [Command, Alias("get")]
        public async Task GetPrefixAsync()
        {
            var prefix = await _messageCommandService.GetGuildPrefixAsync(Context.Guild.Id);

            var embed = new EmbedBuilder()
                .WithTitle($"Command Prefix is set to `{prefix}` for this server")
                .WithDescription($"*Example:* `{prefix}help`")
                .WithCurrentTimestamp()
                .WithFooter($"Executed by {Context.User.Username}#{Context.User.DiscriminatorValue}", Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl());

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }

        [Command, Alias("get")]
        [RequireOwner]
        public async Task GetGuildPrefixAsync(ulong guildId)
        {
            var prefix = await _messageCommandService.GetGuildPrefixAsync(guildId);

            var embed = new EmbedBuilder()
                .WithTitle($"Command Prefix is set to `{prefix}` for this server")
                .WithDescription($"*Example:* `{prefix}help`")
                .WithCurrentTimestamp()
                .WithFooter($"Executed by {Context.User.Username}#{Context.User.DiscriminatorValue}", Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl());

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }

        [Command("change"), Alias("set", "update")]
        public async Task ChangePrefix(string newPrefix)
        {
            var bot = Context.Client.CurrentUser;

            var response = await _mediator.Send(new ChangeGuildMessageCommandsPrefixCommand(Context.Guild.Id, newPrefix));

            var embed = new EmbedBuilder();
            embed.WithAuthor($"{bot.Username} - Guild", bot.GetAvatarUrl() ?? bot.GetDefaultAvatarUrl());
            embed.AddField("Old Prefix", response.OldPrefix);
            embed.AddField("New Prefix", response.NewPrefix);

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }

        [Command("change"), Alias("set", "update")]
        [RequireOwner]
        public async Task ChangePrefix(ulong guildId, string newPrefix)
        {
            var bot = Context.Client.CurrentUser;

            var response = await _mediator.Send(new ChangeGuildMessageCommandsPrefixCommand(guildId, newPrefix));

            var embed = new EmbedBuilder();
            embed.WithAuthor($"{bot.Username} - Guild", bot.GetAvatarUrl() ?? bot.GetDefaultAvatarUrl());
            embed.AddField("Old Prefix", response.OldPrefix);
            embed.AddField("New Prefix", response.NewPrefix);

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}
