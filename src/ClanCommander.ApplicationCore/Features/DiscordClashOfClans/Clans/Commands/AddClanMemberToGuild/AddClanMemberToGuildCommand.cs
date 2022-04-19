namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Commands.AddClanMemberToGuild;

public class AddClanMemberToGuildCommand : IRequest<AddClanMemberToGuildDto>
{
    public ulong GuildId { get; private set; }
    public string ClanId { get; private set; } = default!;
    public string MemberId { get; private set; } = default!;
    public ulong UserId { get; private set; }

    public AddClanMemberToGuildCommand(ulong guildId, string clanId, string memberId, ulong userId)
    {
        GuildId = guildId;
        ClanId = clanId;
        MemberId = memberId;
        UserId = userId;
    }

    internal class AddClanMemberToGuildCommandHandler : IRequestHandler<AddClanMemberToGuildCommand, AddClanMemberToGuildDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IClashOfClansApiClanService _clanApiService;

        public AddClanMemberToGuildCommandHandler(IServiceProvider serviceProvider, IClashOfClansApiClanService clanApiService)
        {
            _serviceProvider = serviceProvider;
            _clanApiService = clanApiService;
        }

        public async Task<AddClanMemberToGuildDto> Handle(AddClanMemberToGuildCommand request, CancellationToken cancellationToken)
        {
            var requestClanId = Entities.ClashOfClans.ClanId.FromString(request.ClanId);
            var requestGuildId = DiscordGuildId.FromUInt64(request.GuildId);
            
            await using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()
                ?? throw new NullReferenceException();

            var clanData = await _clanApiService.GetClanAsync(request.ClanId)
                ?? throw new ArgumentException($"Clan with the Id of '{request.ClanId}' was not found.");   

            var playerData = clanData.MemberList?.SingleOrDefault(x => x.Tag == request.MemberId)
                ?? throw new ArgumentException($"Clan Member with the Id of '{request.MemberId}' was not found.");

            var clan = await dbContext.GuildClans.SingleOrDefaultAsync(guildClan => 
                guildClan.ClanId == requestClanId && guildClan.GuildId == requestGuildId, cancellationToken)
                ?? throw new ArgumentException($"{clanData.Name} is not a registered clan in this server.");

            ClanMemberRole memberRole = playerData!.Role switch
            {
                ClashOfClans.Models.Role.Leader => ClanMemberRole.Leader,
                ClashOfClans.Models.Role.CoLeader => ClanMemberRole.CoLeader,
                ClashOfClans.Models.Role.Admin => ClanMemberRole.Elder,
                ClashOfClans.Models.Role.Member => ClanMemberRole.Member,
                _ => throw new InvalidOperationException("Couldn't establish the member's role in the clan.")
            };

            clan.AddClanMember(PlayerId.FromString(request.MemberId), DiscordUserId.FromUInt64(request.UserId), memberRole);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new AddClanMemberToGuildDto { Id = request.MemberId };
        }
    }
}
