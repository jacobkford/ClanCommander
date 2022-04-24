namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Commands.ChangeClanIdentifierDiscordRole;

public class ChangeClanIdentifierDiscordRoleCommand : IRequest
{
    public ulong GuildId { get; private set; }

    public string ClanId { get; private set; }

    public ulong RoleId { get; private set; }

    public ChangeClanIdentifierDiscordRoleCommand(ulong guildId, string clanId, ulong roleId)
    {
        GuildId = guildId;
        ClanId = clanId;
        RoleId = roleId;
    }

    internal class ChangeClanIdentifierDiscordRoleCommandHandler : IRequestHandler<ChangeClanIdentifierDiscordRoleCommand>
    {
        private readonly IServiceProvider _serviceProvider;

        public ChangeClanIdentifierDiscordRoleCommandHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Unit> Handle(ChangeClanIdentifierDiscordRoleCommand request, CancellationToken cancellationToken)
        {
            var requestClanId = Entities.ClashOfClans.ClanId.FromString(request.ClanId);
            var requestGuildId = DiscordGuildId.FromUInt64(request.GuildId);

            await using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()
                ?? throw new NullReferenceException();

            var clan = await dbContext.GuildClans.SingleOrDefaultAsync(x => x.ClanId == requestClanId && x.GuildId == requestGuildId, cancellationToken)
                ?? throw new ArgumentException($"Clan with the Id of '{request.ClanId}' was not found.");

            clan.ChangeDiscordRole(request.RoleId);

            await dbContext.SaveEntitiesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
