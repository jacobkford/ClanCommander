namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Commands.DisableClanIdentifierDiscordRole;

public class DisableClanIdentifierDiscordRoleCommand : IRequest
{
    public ulong GuildId { get; private set; }

    public string ClanId { get; private set; }

    public DisableClanIdentifierDiscordRoleCommand(ulong guildId, string clanId)
    {
        GuildId = guildId;
        ClanId = clanId;
    }

    internal class DisableClanIdentifierDiscordRoleCommandHandler : IRequestHandler<DisableClanIdentifierDiscordRoleCommand>
    {
        private readonly IServiceProvider _serviceProvider;

        public DisableClanIdentifierDiscordRoleCommandHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Unit> Handle(DisableClanIdentifierDiscordRoleCommand request, CancellationToken cancellationToken)
        {
            var requestClanId = Entities.ClashOfClans.ClanId.FromString(request.ClanId);
            var requestGuildId = DiscordGuildId.FromUInt64(request.GuildId);

            await using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()
                ?? throw new NullReferenceException();

            var clan = await dbContext.GuildClans.SingleOrDefaultAsync(x => x.ClanId == requestClanId && x.GuildId == requestGuildId, cancellationToken)
                ?? throw new ArgumentException($"Clan with the Id of '{request.ClanId}' was not found.");

            clan.DisableDiscordRole();

            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
