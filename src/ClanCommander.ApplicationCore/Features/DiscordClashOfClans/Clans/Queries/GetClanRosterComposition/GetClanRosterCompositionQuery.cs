using System.Collections.Concurrent;

namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Queries.GetClanRosterComposition;

public class GetClanRosterCompositionQuery : IRequest<GetClanRosterCompositionDto>
{
    public string ClanId { get; private set; }

    public GetClanRosterCompositionQuery(string clanId)
    {
        ClanId = clanId.ToUpper();
    }

    internal class GetClanRosterCompositionQueryHandler : IRequestHandler<GetClanRosterCompositionQuery, GetClanRosterCompositionDto?>
    {
        private readonly IClashOfClansApiClanService _clanApiService;
        private readonly IClashOfClansApiPlayerService _playerApiService;

        public GetClanRosterCompositionQueryHandler(IClashOfClansApiClanService clanApiService, IClashOfClansApiPlayerService playerApiService)
        {
            _clanApiService = clanApiService;
            _playerApiService = playerApiService;

        }

        public async Task<GetClanRosterCompositionDto?> Handle(GetClanRosterCompositionQuery request, CancellationToken cancellationToken)
        {
            var clanData = await _clanApiService.GetClanAsync(request.ClanId);
            if (clanData is null)
            {
                throw new ArgumentException($"Clan with the Id of '{request.ClanId}' was not found.");
            }

            var rosterComp = new ConcurrentDictionary<int, int>();

            foreach (var clanMember in clanData.MemberList)
            {
                var player = await _playerApiService.GetPlayerAsync(clanMember.Tag);
                if (player is null) continue;

                rosterComp.AddOrUpdate(player.TownHallLevel, 1, (townHallLevel, count) => count + 1);
            }

            return new GetClanRosterCompositionDto
            {
                ClanId = clanData.Tag,
                ClanName = clanData?.Name ?? "",
                ClanBadgeUrl = clanData?.BadgeUrls.Large?.ToString() ?? "",
                HomeVillageComposition = rosterComp.OrderByDescending(x => x.Key)
                    .ToDictionary(obj => obj.Key, obj => obj.Value),
            };
        }
    }
}
