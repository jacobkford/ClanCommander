namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Queries.GetClanDetails;

public class GetClanDetailsQuery : IRequest<GetClanDetailsDto>
{
    public string Id { get; private set; }

    public GetClanDetailsQuery(string id)
    {
        Id = id.ToUpper();
    }

    internal class GetClanDetailsQueryHandler : IRequestHandler<GetClanDetailsQuery, GetClanDetailsDto>
    {
        private readonly IDbConnection _db;
        private readonly IClashOfClansApiClanService _clanApiService;

        public GetClanDetailsQueryHandler(IConfiguration configuration, IClashOfClansApiClanService clanApiService)
        {
            _db = new NpgsqlConnection(configuration.GetConnectionString("PostgreSQL"));
            _clanApiService = clanApiService;
        }

        // TODO: SQL query & object mapping needs refactoring
        public async Task<GetClanDetailsDto> Handle(GetClanDetailsQuery request, CancellationToken cancellationToken)
        {
            var clanApiData = await _clanApiService.GetClanAsync(request.Id);

            if (clanApiData is null)
            {
                throw new ArgumentException($"Clan with the Id of '{request.Id}' was not found.");
            }

            var clanMemberCount = clanApiData.MemberList?.Count ?? default;
            var clanLeader = clanApiData.MemberList?.SingleOrDefault(x => x.Role == ClashOfClans.Models.Role.Leader)?.Name;
            var clanBadgeUrl = clanApiData.BadgeUrls.Large?.ToString();

            var data = await _db.QuerySingleOrDefaultAsync<GetClanDetailsDto>(
                $@"SELECT 
                    ""{nameof(GuildClan)}"".""{nameof(GuildClan.ClanId)}"" AS ""{nameof(GetClanDetailsDto.Id)}"",
                    ""{nameof(GuildClan.Name)}"" AS ""{nameof(GetClanDetailsDto.Name)}"",
                    ""{nameof(GuildClan.GuildId)}"" AS ""{nameof(GetClanDetailsDto.DiscordGuildId)}"",
                    ""{nameof(GuildClan.DiscordRoleId)}"" AS ""{nameof(GetClanDetailsDto.DiscordRoleId)}"",
                    (
                        SELECT ""{nameof(GuildClanMember.UserId)}"" 
                        FROM ""{ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA}"".""{nameof(GuildClanMember)}"" 
                        WHERE ""{nameof(GuildClanMember.ClanRole)}"" = {ClanMemberRole.Leader.Value} 
                            AND ""{nameof(GuildClan)}"".""{nameof(GuildClan.Id)}"" = ""{nameof(GuildClanMember)}"".""ClanId"" 
                    ) AS ""{nameof(GetClanDetailsDto.LeaderDiscordUserId)}"",
                    (
                        SELECT Count(*) 
                        FROM ""{ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA}"".""{nameof(GuildClanMember)}"" 
                        WHERE ""{nameof(GuildClan)}"".""{nameof(GuildClan.Id)}"" = ""{nameof(GuildClanMember)}"".""ClanId"" 
                    ) AS ""{nameof(GetClanDetailsDto.RegisteredMemberCount)}"" 
                    FROM ""{ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA}"".""{nameof(GuildClan)}""  
                    WHERE ""{nameof(GuildClan)}"".""{nameof(GuildClan.ClanId)}"" = @ClanId;",
                new
                {
                    @ClanId = request.Id,
                });

            if (data is null)
            {
                return new GetClanDetailsDto 
                { 
                    Id = request.Id,
                    Name = clanApiData.Name,
                    MemberCount = clanMemberCount,
                    ClanBadgeUrl = clanBadgeUrl,
                    LeaderName = clanLeader,
                };
            }

            data.Registered = true;
            data.MemberCount = clanMemberCount;
            data.ClanBadgeUrl = clanBadgeUrl;
            data.LeaderName = clanLeader;
            return data;
        }
    }
}
