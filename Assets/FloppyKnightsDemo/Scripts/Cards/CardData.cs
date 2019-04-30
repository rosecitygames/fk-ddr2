using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;
using FloppyKnights.Agents;

namespace FloppyKnights.Cards
{
    public class CardData : ICardData
    {
        
        string IDescribable.DisplayName
        {
            get
            {
                return Displayname;
            }
        }

        protected string Displayname { get; set; }

        string IDescribable.Description
        {
            get
            {
                return Description;
            }
        }

        protected string Description { get; set; }

        List<IAttribute> IStatsCollection.Stats
        {
            get
            {
                return Stats.Attributes;
            }
        }

        IAttributeCollection Stats { get; set; }

        IAttribute IStatsCollection.GetStat(string id)
        {
            return Stats.GetAttribute(id);
        }

        List<ICardAction> ICardActionCollection.CardActions
        {
            get
            {
                List<ICardAction> cardActionsCopy = new List<ICardAction>();
                foreach (ICardAction cardAction in CardActions)
                {
                    cardActionsCopy.Add(cardAction.Copy());
                }
                return cardActionsCopy;
            }
        }

        List<ICardAction> CardActions { get; set; }

        IBrain ICardData.AgentBrain
        {
            get
            {
                return AgentBrain;
            }
        }

        IBrain AgentBrain { get; set; }
        
        Sprite ICardData.AgentSprite
        {
            get
            {
                return AgentSprite;
            }
        }
        Sprite AgentSprite { get; set; }

        ICardData ICardData.Copy()
        {
            return new CardData
            {
                Displayname = Displayname,
                Description = Description,
                Stats = Stats,
                CardActions = CardActions
            };
        }

        public static ICardData Create(ICardData sourceData)
        {
            return new CardData
            {
                Displayname = sourceData.DisplayName,
                Description = sourceData.Description,
                Stats = new AttributeCollection(sourceData.Stats),
                CardActions = sourceData.CardActions,
                AgentBrain = sourceData.AgentBrain,
                AgentSprite = sourceData.AgentSprite
            };
        }
    }
}
