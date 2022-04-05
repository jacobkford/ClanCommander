namespace ClanCommander.ApplicationCore.Interfaces;

public interface IClashOfClansApiClanService
{
    Task<ClashOfClans.Models.Clan?> GetClanAsync(string id);
}
