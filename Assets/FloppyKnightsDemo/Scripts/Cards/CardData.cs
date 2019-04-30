using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;

namespace RCG.Demo.FloppyKnights.Cards
{
    [System.Serializable]
    public class CardData : ICardData
    {
        [SerializeField]
        string displayName;

        string IDescribable.DisplayName
        {
            get
            {
                return DisplayName;
            }
        }

        protected string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
            }
        }

        [SerializeField]
        [TextArea]
        string description;
        string IDescribable.Description
        {
            get
            {
                return Description;
            }
        }

        protected string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        [SerializeField]
        List<AbstractCardAction> cardActions = new List<AbstractCardAction>();

        List<ICardAction> ICardData.CardActions
        {
            get
            {
                return CardActions;
            }
        }

        List<ICardAction> CardActions
        {
            get
            {
                List<ICardAction> cardActionsCopy = new List<ICardAction>();
                foreach(ICardAction cardAction in cardActions)
                {
                    cardActionsCopy.Add(cardAction.Copy());
                }
                return cardActionsCopy;
            }
        }


        ICardData ICardData.Copy()
        {
            return new CardData
            {
                cardActions = cardActions
            };
        }

    }
}
