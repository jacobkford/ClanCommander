using ClashOfClans.Models;

namespace ClanCommander.ApplicationCore.Services;

public interface IClashOfClansService
{
    Task<Player?> FindPlayer(string id);
}

