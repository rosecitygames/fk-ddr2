using RCG.Attributes;
using System.Collections.Generic;

namespace FloppyKnights
{
    public interface ITeam : IDescribable, IGroupMember, ITurnTaker
    {
        int TeamId { get; }
        List<ICardPlayer> TeamMembers { get; }
    }
}
