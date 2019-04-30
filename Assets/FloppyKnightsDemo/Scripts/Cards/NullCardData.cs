using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;
using FloppyKnights.Agents;

namespace FloppyKnights.Cards
{
    public class NullCardData : ICardData
    {
        string IDescribable.DisplayName { get { return ""; } }
        string IDescribable.Description { get { return ""; } }

        IAttributeCollection stats = new AttributeCollection();
        List<IAttribute> IStatsCollection.Stats { get { return stats.Attributes; } }
        IAttribute IStatsCollection.GetStat(string id) { return stats.GetAttribute(id); }

        List<ICardAction> cardActions = new List<ICardAction>();
        List<ICardAction> ICardActionCollection.CardActions { get { return cardActions; } }

        IBrain agentBrain = new NullBrain();
        IBrain ICardData.AgentBrain { get { return agentBrain; } }

        Sprite ICardData.AgentSprite { get { return null; } }

        ICardData ICardData.Copy() { return new NullCardData(); }
    }
}
