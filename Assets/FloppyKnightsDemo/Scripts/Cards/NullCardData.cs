using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;
using FloppyKnights.Agents;

namespace FloppyKnights.Cards
{
    public class NullCardData : ICardData
    {
        string IDescribable.DisplayName { get => ""; }
        string IDescribable.Description { get => ""; }

        IAttributeCollection stats = new AttributeCollection();
        List<IAttribute> IStatsCollection.Stats { get => stats.Attributes; }
        IAttribute IStatsCollection.GetStat(string id) { return stats.GetAttribute(id); }

        List<ICardAction> cardActions = new List<ICardAction>();
        List<ICardAction> ICardActionCollection.CardActions { get => cardActions; }

        Sprite ICardData.AgentSprite { get => null; }

        ICardData ICardData.Copy() { return new NullCardData(); }
    }
}
