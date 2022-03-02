using ClanCommander.ApplicationCore.Features.Discord.Guilds.ClientEvents;
using MediatR;

namespace ClanCommander.DiscordBot.Services;

public class GuildHandler : DiscordClientService
{
    private readonly IMediator _mediator;

    public GuildHandler(DiscordSocketClient client, ILogger<DiscordClientService> logger, IMediator mediator) : base(client, logger)
    {
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Client.JoinedGuild += HandleJoinedGuild;
        Client.LeftGuild += HandleLeftGuild;
        Client.GuildUpdated += HandleUpdatedGuild;
    }

    private Task HandleUpdatedGuild(SocketGuild beforeGuild, SocketGuild afterGuild)
    {
        return _mediator.Publish(new DiscordGuildUpdatedClientEvent(
                       beforeGuild.Id,
                       beforeGuild.Name,
                       afterGuild.Name,
                       beforeGuild.OwnerId,
                       afterGuild.OwnerId));
    }

    private Task HandleJoinedGuild(SocketGuild guild)
    {
        return _mediator.Publish(new JoinedDiscordGuildClientEvent(guild.Id, guild.Name, guild.OwnerId));
    }

    private Task HandleLeftGuild(SocketGuild guild)
    {
        return _mediator.Publish(new LeftDiscordGuildClientEvent(guild.Id));
    }


}
