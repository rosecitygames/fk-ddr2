using RCG.Agents;
using RCG.Attributes;
using UnityEngine;

namespace RCG.Demo.FloppyKnights
{
    public interface ICardPlayer : IDescribable, IGroupMember, ITurnTaker
    {
        int TeamId { get; }
        IUnitAgent TargetAgent { get; }
        Vector3Int TargetLocation { get; }
    }
}
