using RCG.Attributes;
using FloppyKnights.Agents;
using UnityEngine;

namespace FloppyKnights.Cards
{
    public interface ICardData : IDescribable, IStatsCollection, ICardActionCollection
    {
        IBrain AgentBrain { get; }
        Sprite AgentSprite { get; } // TODO : Eventually will want to use some new IAnimatable or IRenderable
        ICardData Copy();
    }
}
