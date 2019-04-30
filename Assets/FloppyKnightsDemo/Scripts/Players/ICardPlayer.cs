using RCG.Agents;
using RCG.Attributes;
using UnityEngine;
using FloppyKnights.Agents;
using FloppyKnights.Cards;

namespace FloppyKnights
{
    public interface ICardPlayer : IDescribable, IGroupMember, ITurnTaker
    {
        int TeamId { get; }
        ICardAgent TargetAgent { get; }
        Vector3Int TargetLocation { get; }

        void AddCardToDiscardDeck(ICardData cardData);

    }
}
