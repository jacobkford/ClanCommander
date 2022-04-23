namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.DomainEvents;

internal class DiscordGuildRegisteredEventHandler : INotificationHandler<DiscordGuildRegisteredEvent>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IDiscordUserService _userService;

    public DiscordGuildRegisteredEventHandler(IServiceProvider serviceProvider, IDiscordUserService userService)
    {
        _serviceProvider = serviceProvider;
        _userService = userService;
    }

    public async Task Handle(DiscordGuildRegisteredEvent notification, CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()
            ?? throw new NullReferenceException();

        var guildMessageCommandEntity = new GuildMessageCommands(notification.GuildId);
        await dbContext.AddAsync(guildMessageCommandEntity, cancellationToken);

        var ownerUsername = await _userService.GetUsername(notification.OwnerId.Value);
        var ownerEntity = new DiscordUser(notification.OwnerId, ownerUsername ?? "");
        await dbContext.AddAsync(ownerEntity, cancellationToken);

        // TODO: Send to log discord channel 
        await dbContext.SaveChangesAsync(cancellationToken);
        return;
    }
}
