namespace ClanCommander.ApplicationCore.Entities.ClashOfClans;

internal abstract class ClanMemberRole : SmartEnum<ClanMemberRole>
{
    public static readonly ClanMemberRole Member = new MemberRole();
    public static readonly ClanMemberRole Elder = new ElderRole();
    public static readonly ClanMemberRole CoLeader = new CoLeaderRole();
    public static readonly ClanMemberRole Leader = new LeaderRole();

    private ClanMemberRole(string name, int value) : base(name, value)
    {
    }

    /// <summary>
    /// Gets the Clash Of Clans API value for this role.
    /// </summary>
    /// <returns>Clash Of Clans Role API <see cref="string"/> value</returns>
    public abstract string GetApiValue();

    /// <summary>
    /// Checks if this <see cref="ClanMemberRole"/> can be promoted or demoted to the requested <see cref="ClanMemberRole"/>.
    /// </summary>
    /// <param name="next">The requested role to transition to.</param>
    /// <returns><see cref="Boolean"/></returns>
    public abstract bool CanTransitionTo(ClanMemberRole next);

    /// <summary>
    /// Checks whether this <see cref="ClanMemberRole"/> can promote a user with the requested <see cref="ClanMemberRole"/>.
    /// </summary>
    /// <param name="roleToChange">The user to promote's <see cref="ClanMemberRole"/>.</param>
    /// <returns><see cref="Boolean"/></returns>
    public abstract bool IsAbleToPromote(ClanMemberRole roleToChange);

    /// <summary>
    /// Checks whether this <see cref="ClanMemberRole"/> can demote a user with the requested <see cref="ClanMemberRole"/>.
    /// </summary>
    /// <param name="roleToChange">The user to demote's <see cref="ClanMemberRole"/>.</param>
    /// <returns><see cref="Boolean"/></returns>
    public abstract bool IsAbleToDemote(ClanMemberRole roleToChange);

    /// <summary>
    /// Promotes to the next role if there is one
    /// </summary>
    /// <returns>The next <see cref="ClanMemberRole"/> if there is one, else it returns <see cref="null"/></returns>
    public abstract ClanMemberRole? Promote();

    /// <summary>
    /// Demotes to the previous role if there is one
    /// </summary>
    /// <returns>The previous <see cref="ClanMemberRole"/> if there is one, else it returns <see cref="null"/></returns>
    public abstract ClanMemberRole? Demote();

    private sealed class MemberRole : ClanMemberRole
    {
        public MemberRole() : base("Member", 0)
        {
        }

        public override string GetApiValue()
        {
            return "Member";
        }

        public override bool CanTransitionTo(ClanMemberRole next)
        {
            return next == Elder;
        }

        public override bool IsAbleToPromote(ClanMemberRole roleToChange)
        {
            return false;
        }

        public override bool IsAbleToDemote(ClanMemberRole roleToChange)
        {
            return false;
        }

        public override ClanMemberRole? Promote() => Elder;

        public override ClanMemberRole? Demote() => null;
    }

    private sealed class ElderRole : ClanMemberRole
    {
        public ElderRole() : base("Elder", 1)
        {
        }

        public override string GetApiValue()
        {
            return "Admin";
        }

        public override bool CanTransitionTo(ClanMemberRole next)
        {
            return next == Member || next == CoLeader;
        }

        public override bool IsAbleToPromote(ClanMemberRole roleToChange)
        {
            return roleToChange == Member;
        }

        public override bool IsAbleToDemote(ClanMemberRole roleToChange)
        {
            return false;
        }

        public override ClanMemberRole? Promote() => CoLeader;

        public override ClanMemberRole? Demote() => Member;
    }

    private sealed class CoLeaderRole : ClanMemberRole
    {
        public CoLeaderRole() : base("Co-Leader", 2)
        {
        }

        public override string GetApiValue()
        {
            return "CoLeader";
        }

        public override bool CanTransitionTo(ClanMemberRole next)
        {
            return next == Elder || next == Leader;
        }

        public override bool IsAbleToPromote(ClanMemberRole roleToChange)
        {
            return roleToChange == Member || roleToChange == Elder;
        }

        public override bool IsAbleToDemote(ClanMemberRole roleToChange)
        {
            return roleToChange == Elder;
        }

        public override ClanMemberRole? Promote() => Leader;

        public override ClanMemberRole? Demote() => Elder;
    }

    private sealed class LeaderRole : ClanMemberRole
    {
        public LeaderRole() : base("Leader", 3)
        {
        }

        public override string GetApiValue()
        {
            return "Leader";
        }

        public override bool CanTransitionTo(ClanMemberRole next)
        {
            return next == CoLeader;
        }

        public override bool IsAbleToPromote(ClanMemberRole roleToChange)
        {
            return roleToChange != Leader;
        }

        public override bool IsAbleToDemote(ClanMemberRole roleToChange)
        {
            return roleToChange == Elder || roleToChange == CoLeader;
        }

        public override ClanMemberRole? Promote() => null;

        public override ClanMemberRole? Demote() => CoLeader;
    }
}
