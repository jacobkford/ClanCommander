/*namespace ClanCommander.DiscordBot.Services;

public class GuildHandler : DiscordClientService
{
    public GuildHandler(DiscordSocketClient client, ILogger<DiscordClientService> logger) : base(client, logger)
    {
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Client.JoinedGuild += HandleJoinedGuild;
        Client.LeftGuild += HandleLeftGuild;
    }
    private Task HandleJoinedGuild(SocketGuild guild)
    {
        throw new NotImplementedException();
    }

    private Task HandleLeftGuild(SocketGuild guild)
    {
        throw new NotImplementedException();
    }

    
}*/
