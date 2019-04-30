using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;

namespace RCG.Demo.FloppyKnights.Cards
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


        ICardData ICardData.Copy()
        {
            return new CardData
            {
                Displayname = Displayname,
                Description = Description,
                CardActions = CardActions
            };
        }

        public static ICardData Create(ICardData sourceData)
        {
            return new CardData
            {
                Displayname = sourceData.DisplayName,
                Description = sourceData.Description,
                CardActions = sourceData.CardActions
            };
        }
    }
}
