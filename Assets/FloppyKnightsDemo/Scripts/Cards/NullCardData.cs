using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;
using FloppyKnights.Agents;

namespace FloppyKnights.Cards
{
    public class NullCardData : ICardData
    {
        string IIdable.Id => "";

        string IDescribable.DisplayName => "";
        string IDescribable.Description => "";

        IAttributeCollection stats = new AttributeCollection();
        List<IAttribute> IStatsCollection.Stats => stats.Attributes;
        IAttribute IStatsCollection.GetStat(string id) => stats.GetAttribute(id);

        List<ICardAction> cardActions = new List<ICardAction>();
        List<ICardAction> ICardActionCollection.CardActions => cardActions;
        bool ICardActionCollection.HasCardAction(string cardActionId) => false;

        int ICardData.Cost => 0;
        Sprite ICardData.AgentSprite => null;

        ICardData ICardData.Copy() { return new NullCardData(); }
    }
}
