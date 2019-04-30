using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;

namespace RCG.Demo.FloppyKnights.Cards
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Floppy Knights/Card Data")]
    public class ScriptableCardData : ScriptableObject, ICardData
    {
        [SerializeField]
        string displayName;

        string IDescribable.DisplayName
        {
            get
            {
                return displayName;
            }
        }

        [SerializeField]
        [TextArea]
        string description;
        string IDescribable.Description
        {
            get
            {
                return description;
            }
        }

        [SerializeField]
        List<ScriptableCardAction> cardActions = new List<ScriptableCardAction>();

        List<ICardAction> ICardActionCollection.CardActions
        {
            get
            {
                List<ICardAction> cardActionsCopy = new List<ICardAction>();
                foreach (ICardAction cardAction in cardActions)
                {
                    cardActionsCopy.Add(cardAction.Copy());
                }
                return cardActionsCopy;
            }
        }

        ICardData ICardData.Copy()
        {
            return CardData.Create(this);
        }
    }
}
