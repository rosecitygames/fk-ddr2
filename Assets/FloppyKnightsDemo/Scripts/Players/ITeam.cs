using RCG.Attributes;
using System.Collections.Generic;

namespace FloppyKnights.CardPlayers
{
    public interface ITeam : IDescribable, IGroupMember, ITurnTaker
    {
        List<ICardPlayer> TeamMembers { get; }
    }
}
