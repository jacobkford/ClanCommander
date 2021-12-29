namespace ClanCommander.ApplicationCore.Interfaces;

public interface IClashOfClansService
{
    Task<Player?> FindPlayer(string id);
}

