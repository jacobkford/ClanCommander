using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;

namespace ClanCommander.DiscordBot.Services;

public class CommandHandler : DiscordClientService
{
    private readonly IServiceProvider _provider;
    private readonly CommandService _commandService;
    private readonly IConfiguration _configuration;

    public CommandHandler(
        DiscordSocketClient client,
        ILogger<CommandHandler> logger,
        IServiceProvider provider,
        CommandService commandService,
        IConfiguration configuration) : base(client, logger)
    {
        _provider = provider;
        _commandService = commandService;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Client.MessageReceived += HandleMessage;
        _commandService.CommandExecuted += CommandExecutedAsync;
        await _commandService.AddModulesAsync(Assembly.Load("ClanCommander.Application"), _provider);
    }
    private async Task HandleMessage(SocketMessage incomingMessage)
    {
        if (incomingMessage is not SocketUserMessage message) return;
        if (message.Source != MessageSource.User) return;

        int argPos = 0;

        var executedWithPrefix = message.HasStringPrefix(_configuration["Prefix"], ref argPos);
        var executedWithBotMention = message.HasMentionPrefix(Client.CurrentUser, ref argPos);

        if (!executedWithPrefix && !executedWithBotMention) return;

        var context = new SocketCommandContext(Client, message);

        await _commandService.ExecuteAsync(context, argPos, _provider);
    }

    private async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
    {
        Logger.LogInformation("User {user} attempted to use command {command}", context.User, command.Value.Name);

        if (!command.IsSpecified || result.IsSuccess)
            return;

        await context.Channel.SendMessageAsync($"Error: {result}");
    } 
}

