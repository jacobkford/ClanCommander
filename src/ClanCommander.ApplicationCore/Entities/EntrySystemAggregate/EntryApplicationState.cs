namespace ClanCommander.ApplicationCore.Entities;

internal sealed class EntryApplicationState : SmartEnum<EntryApplicationState>
{
    public static readonly EntryApplicationState Active = new(nameof(Active), 1);
    public static readonly EntryApplicationState Success = new(nameof(Success), 2);
    public static readonly EntryApplicationState Failure = new(nameof(Failure), 3);

    private EntryApplicationState(string name, int value) : base(name, value)
    {
    }
}
