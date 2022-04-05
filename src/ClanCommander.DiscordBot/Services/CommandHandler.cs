using ClanCommander.ApplicationCore.Interfaces;

namespace ClanCommander.DiscordBot.Services;

public class CommandHandler : DiscordClientService
{
    private readonly IServiceProvider _provider;
    private readonly CommandService _commandService;
    private readonly IConfiguration _configuration;
    private readonly IMessageCommandService _messageCommandService;

    public CommandHandler(
        DiscordSocketClient client,
        ILogger<CommandHandler> logger,
        IServiceProvider provider,
        CommandService commandService,
        IConfiguration configuration,
        IMessageCommandService messageCommandService) : base(client, logger)
    {
        _provider = provider;
        _commandService = commandService;
        _configuration = configuration;
        _messageCommandService = messageCommandService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Client.MessageReceived += HandleMessage;
        _commandService.CommandExecuted += CommandExecutedAsync;
        await _commandService.AddModulesAsync(Assembly.GetExecutingAssembly(), _provider);
    }
    private async Task HandleMessage(SocketMessage incomingMessage)
    {
        if (incomingMessage is not SocketUserMessage message) return;
        if (message.Source != MessageSource.User) return;

        int argPos = 0;

        var prefix = await _messageCommandService.GetGuildPrefixAsync((incomingMessage.Channel as SocketGuildChannel)?.Guild.Id);

        var executedWithPrefix = message.HasStringPrefix(prefix, ref argPos);
        var executedWithBotMention = message.HasMentionPrefix(Client.CurrentUser, ref argPos);

        if (!executedWithPrefix && !executedWithBotMention) return;

        var context = new SocketCommandContext(Client, message);

        await _commandService.ExecuteAsync(context, argPos, _provider);
    }

    private async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, ICommandResult result)
    {
        Logger.LogInformation("User {user} attempted to use command {command}", context.User, command.Value.Name);

        if (!command.IsSpecified || result.IsSuccess)
            return;

        var errorEmbed = new ErrorEmbedBuilder()
            .WithTitle($"❌ Error: {result.ErrorReason}")
            .WithFooter($"Executed by {context.User.Username}#{context.User.DiscriminatorValue}", context.User.GetAvatarUrl() ?? context.User.GetDefaultAvatarUrl())
            .WithCurrentTimestamp();

        await context.Channel.SendMessageAsync(embed: errorEmbed.Build());
    } 
}

