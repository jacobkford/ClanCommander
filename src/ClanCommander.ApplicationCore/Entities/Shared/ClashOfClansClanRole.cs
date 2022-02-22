namespace ClanCommander.ApplicationCore.Entities.ClashOfClans;

internal abstract class ClashOfClansClanRole : SmartEnum<ClashOfClansClanRole>
{
    public static readonly ClashOfClansClanRole Member = new MemberRole();
    public static readonly ClashOfClansClanRole Elder = new ElderRole();
    public static readonly ClashOfClansClanRole CoLeader = new CoLeaderRole();
    public static readonly ClashOfClansClanRole Leader = new LeaderRole();

    private ClashOfClansClanRole(string name, int value) : base(name, value)
    {
    }

    /// <summary>
    /// Gets the Clash Of Clans API value for this role.
    /// </summary>
    /// <returns>Clash Of Clans Role API <see cref="string"/> value</returns>
    public abstract string GetApiValue();

    /// <summary>
    /// Checks if this <see cref="ClashOfClansClanRole"/> can be promoted or demoted to the requested <see cref="ClashOfClansClanRole"/>.
    /// </summary>
    /// <param name="next">The requested role to transition to.</param>
    /// <returns><see cref="Boolean"/></returns>
    public abstract bool CanTransitionTo(ClashOfClansClanRole next);

    /// <summary>
    /// Checks whether this <see cref="ClashOfClansClanRole"/> can promote a user with the requested <see cref="ClashOfClansClanRole"/>.
    /// </summary>
    /// <param name="roleToChange">The user to promote's <see cref="ClashOfClansClanRole"/>.</param>
    /// <returns><see cref="Boolean"/></returns>
    public abstract bool IsAbleToPromote(ClashOfClansClanRole roleToChange);

    /// <summary>
    /// Checks whether this <see cref="ClashOfClansClanRole"/> can demote a user with the requested <see cref="ClashOfClansClanRole"/>.
    /// </summary>
    /// <param name="roleToChange">The user to demote's <see cref="ClashOfClansClanRole"/>.</param>
    /// <returns><see cref="Boolean"/></returns>
    public abstract bool IsAbleToDemote(ClashOfClansClanRole roleToChange);

    /// <summary>
    /// Promotes to the next role if there is one
    /// </summary>
    /// <returns>The next <see cref="ClashOfClansClanRole"/> if there is one, else it returns <see cref="null"/></returns>
    public abstract ClashOfClansClanRole? Promote();

    /// <summary>
    /// Demotes to the previous role if there is one
    /// </summary>
    /// <returns>The previous <see cref="ClashOfClansClanRole"/> if there is one, else it returns <see cref="null"/></returns>
    public abstract ClashOfClansClanRole? Demote();

    private sealed class MemberRole : ClashOfClansClanRole
    {
        public MemberRole() : base("Member", 0)
        {
        }

        public override string GetApiValue()
        {
            return "Member";
        }

        public override bool CanTransitionTo(ClashOfClansClanRole next)
        {
            return next == Elder;
        }

        public override bool IsAbleToPromote(ClashOfClansClanRole roleToChange)
        {
            return false;
        }

        public override bool IsAbleToDemote(ClashOfClansClanRole roleToChange)
        {
            return false;
        }

        public override ClashOfClansClanRole? Promote() => Elder;

        public override ClashOfClansClanRole? Demote() => null;
    }

    private sealed class ElderRole : ClashOfClansClanRole
    {
        public ElderRole() : base("Elder", 1)
        {
        }

        public override string GetApiValue()
        {
            return "Admin";
        }

        public override bool CanTransitionTo(ClashOfClansClanRole next)
        {
            return next == Member || next == CoLeader;
        }

        public override bool IsAbleToPromote(ClashOfClansClanRole roleToChange)
        {
            return roleToChange == Member;
        }

        public override bool IsAbleToDemote(ClashOfClansClanRole roleToChange)
        {
            return false;
        }

        public override ClashOfClansClanRole? Promote() => CoLeader;

        public override ClashOfClansClanRole? Demote() => Member;
    }

    private sealed class CoLeaderRole : ClashOfClansClanRole
    {
        public CoLeaderRole() : base("Co-Leader", 2)
        {
        }

        public override string GetApiValue()
        {
            return "CoLeader";
        }

        public override bool CanTransitionTo(ClashOfClansClanRole next)
        {
            return next == Elder || next == Leader;
        }

        public override bool IsAbleToPromote(ClashOfClansClanRole roleToChange)
        {
            return roleToChange == Member || roleToChange == Elder;
        }

        public override bool IsAbleToDemote(ClashOfClansClanRole roleToChange)
        {
            return roleToChange == Elder;
        }

        public override ClashOfClansClanRole? Promote() => Leader;

        public override ClashOfClansClanRole? Demote() => Elder;
    }

    private sealed class LeaderRole : ClashOfClansClanRole
    {
        public LeaderRole() : base("Leader", 3)
        {
        }

        public override string GetApiValue()
        {
            return "Leader";
        }

        public override bool CanTransitionTo(ClashOfClansClanRole next)
        {
            return next == CoLeader;
        }

        public override bool IsAbleToPromote(ClashOfClansClanRole roleToChange)
        {
            return roleToChange != Leader;
        }

        public override bool IsAbleToDemote(ClashOfClansClanRole roleToChange)
        {
            return roleToChange == Elder || roleToChange == CoLeader;
        }

        public override ClashOfClansClanRole? Promote() => null;

        public override ClashOfClansClanRole? Demote() => CoLeader;
    }
}
