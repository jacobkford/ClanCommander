namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.Commands.RegisterDiscordGuild;

public class RegisterDiscordGuildCommand : IRequest<RegisterDiscordGuildDto>
{
    public ulong GuildId { get; private set; }

    public string GuildName { get; private set; }

    public ulong GuildOwnerId { get; private set; }

    public RegisterDiscordGuildCommand(ulong guildId, string guildName, ulong guildOwnerId)
    {
        GuildId = guildId;
        GuildName = guildName;
        GuildOwnerId = guildOwnerId;
    }

    internal class RegisterDiscordGuildCommandHandler : IRequestHandler<RegisterDiscordGuildCommand, RegisterDiscordGuildDto>
    {
        private readonly IServiceProvider _serviceProvider;

        public RegisterDiscordGuildCommandHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<RegisterDiscordGuildDto> Handle(RegisterDiscordGuildCommand request, CancellationToken cancellationToken)
        {
            var guildId = DiscordGuildId.FromUInt64(request.GuildId);
            var ownerId = DiscordUserId.FromUInt64(request.GuildOwnerId);

            await using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()
                ?? throw new NullReferenceException();

            var guildAlreadyExists = await dbContext.DiscordGuilds.AnyAsync(x => x.GuildId == guildId, cancellationToken);

            if (guildAlreadyExists)
            {
                throw new ArgumentException("Discord server is already registered");
            }

            var guild = new RegisteredDiscordGuild(guildId, request.GuildName, ownerId);

            await dbContext.AddAsync(guild, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new RegisterDiscordGuildDto { GuildId = request.GuildId };
        }
    }
}
