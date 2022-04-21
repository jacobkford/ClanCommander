namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Queries.GetClanDetails;

public class GetClanDetailsQuery : IRequest<GetClanDetailsDto>
{
    public string ClanId { get; private set; }
    public ulong GuildId { get; private set; }

    public GetClanDetailsQuery(string clanId, ulong guildId)
    {
        ClanId = clanId.ToUpper();
        GuildId = guildId;
    }

    internal class GetClanDetailsByIdQueryHandler : IRequestHandler<GetClanDetailsQuery, GetClanDetailsDto>
    {
        private readonly IDbConnection _db;
        private readonly IClashOfClansApiClanService _clanApiService;

        public GetClanDetailsByIdQueryHandler(IConfiguration configuration, IClashOfClansApiClanService clanApiService)
        {
            _db = new NpgsqlConnection(configuration.GetConnectionString("PostgreSQL"));
            _clanApiService = clanApiService;
        }

        // TODO: SQL query & object mapping needs refactoring
        public async Task<GetClanDetailsDto> Handle(GetClanDetailsQuery request, CancellationToken cancellationToken)
        {
            var clanData = await _clanApiService.GetClanAsync(request.ClanId);
            if (clanData is null)
            {
                throw new ArgumentException($"Clan with the Id of '{request.ClanId}' was not found.");
            }

            var clanName = clanData!.Name;
            var clanRosterData = clanData.MemberList ?? throw new ArgumentNullException($"{clanName}'s roster couldn't be retrieved");
            var clanRosterCount = clanRosterData.Count;
            var clanBadgeUrl = clanData.BadgeUrls.Large?.ToString() ?? "";

            var clanLeader = clanRosterData!.SingleOrDefault(x => x.Role == ClashOfClans.Models.Role.Leader)
                ?? throw new ArgumentNullException($"{clanName}'s clan Leader couldn't be found");
            var clanLeaderId = clanLeader.Tag;
            var clanLeaderName = clanLeader.Name;

            var sqlQuery = $@"SELECT 
                                ""{nameof(GuildClan)}"".""{nameof(GuildClan.ClanId)}"" AS ""{nameof(GetClanDetailsDto.Id)}"",
                                ""{nameof(GuildClan.Name)}"" AS ""{nameof(GetClanDetailsDto.Name)}"",
                                ""{nameof(GuildClan.GuildId)}"" AS ""{nameof(GetClanDetailsDto.DiscordGuildId)}"",
                                ""{nameof(GuildClan.DiscordRoleId)}"" AS ""{nameof(GetClanDetailsDto.DiscordRoleId)}"",
                                (
                                    SELECT ""{nameof(GuildClanMember.UserId)}"" 
                                    FROM ""{ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA}"".""{nameof(GuildClanMember)}"" 
                                    WHERE ""{nameof(GuildClanMember.MemberId)}"" = @ClanLeader 
                                        AND ""{nameof(GuildClan)}"".""{nameof(GuildClan.Id)}"" = ""{nameof(GuildClanMember)}"".""ClanId"" 
                                ) AS ""{nameof(GetClanDetailsDto.LeaderDiscordUserId)}"",
                                (
                                    SELECT Count(*) 
                                    FROM ""{ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA}"".""{nameof(GuildClanMember)}"" 
                                    WHERE ""{nameof(GuildClan)}"".""{nameof(GuildClan.Id)}"" = ""{nameof(GuildClanMember)}"".""ClanId"" 
                                ) AS ""{nameof(GetClanDetailsDto.RegisteredMemberCount)}"" 
                                FROM ""{ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA}"".""{nameof(GuildClan)}""  
                                WHERE ""{nameof(GuildClan)}"".""{nameof(GuildClan.ClanId)}"" = @ClanId 
                                    AND ""{nameof(GuildClan)}"".""{nameof(GuildClan.GuildId)}"" = @GuildId;";

            var data = await _db.QuerySingleOrDefaultAsync<GetClanDetailsDto>(sqlQuery, new { @ClanId = request.ClanId, @GuildId = (decimal)request.GuildId, @ClanLeader = clanLeader!.Tag });
            if (data is null)
            {
                return new GetClanDetailsDto 
                { 
                    Id = request.ClanId,
                    Name = clanName,
                    MemberCount = clanRosterCount,
                    ClanBadgeUrl = clanBadgeUrl,
                    LeaderName = clanLeaderName,
                };
            }

            data.Registered = true;
            data.MemberCount = clanRosterCount;
            data.ClanBadgeUrl = clanBadgeUrl;
            data.LeaderName = clanLeaderName;
            return data;
        }
    }
}
