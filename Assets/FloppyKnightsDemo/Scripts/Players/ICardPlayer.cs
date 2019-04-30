using RCG.Agents;
using RCG.Attributes;
using UnityEngine;
using FloppyKnights.Agents;

namespace FloppyKnights
{
    public interface ICardPlayer : IDescribable, IGroupMember, ITurnTaker
    {
        int TeamId { get; }
        ICardAgent TargetAgent { get; }
        Vector3Int TargetLocation { get; }
    }
}
