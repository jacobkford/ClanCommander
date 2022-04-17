namespace ClanCommander.ApplicationCore.Interfaces;

public interface IClashOfClansApiPlayerService
{
    Task<ClashOfClans.Models.Player?> GetPlayerAsync(string id);
}
