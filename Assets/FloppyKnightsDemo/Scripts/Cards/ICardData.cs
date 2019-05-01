using RCG.Attributes;
using FloppyKnights.Agents;
using UnityEngine;

namespace FloppyKnights.Cards
{
    public interface ICardData : IDescribable, IStatsCollection, ICardActionCollection
    {
        Sprite AgentSprite { get; } // TODO : Eventually will want to use some new IAnimatable or IRenderable
        ICardData Copy();
    }
}
