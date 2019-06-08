using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;
using FloppyKnights.Agents;
using Sirenix.OdinInspector;

namespace FloppyKnights.Cards
{
    [System.Serializable]
    public class CardData : ICardData
    {
        string IIdable.Id => Id;
        [ShowInInspector, ReadOnly]
        protected string Id { get; set; }

        string IDescribable.DisplayName => Displayname;
        protected string Displayname { get; set; }

        string IDescribable.Description => Description;
        protected string Description { get; set; }

        List<IAttribute> IStatsCollection.Stats => Stats.Attributes;
        IAttributeCollection Stats { get; set; }

        IAttribute IStatsCollection.GetStat(string id) => Stats.GetAttribute(id);

        List<ICardAction> ICardActionCollection.CardActions => CardActions;
        List<ICardAction> CardActions { get; set; }

        int ICardData.Cost => Cost;
        int Cost { get; set; }

        Sprite ICardData.AgentSprite => AgentSprite;
        Sprite AgentSprite { get; set; }

        ICardData ICardData.Copy()
        {
            return new CardData
            {
                Id = Id,
                Displayname = Displayname,
                Description = Description,
                Stats = Stats,
                CardActions = CardActions,
                Cost = Cost,
                AgentSprite = AgentSprite
            };
        }

        public static ICardData Create(ICardData sourceData)
        {
            return new CardData
            {
                Id = sourceData.Id,
                Displayname = sourceData.DisplayName,
                Description = sourceData.Description,
                Stats = new AttributeCollection(sourceData.Stats),
                CardActions = sourceData.CardActions,
                Cost = sourceData.Cost,
                AgentSprite = sourceData.AgentSprite
            };
        }
    }
}
