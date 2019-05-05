using FloppyKnights.CardPlayers;
using UnityEngine;

namespace FloppyKnights.Cards
{
    [System.Serializable]
    public class AddCard : AbstractCardAction
    {
        [SerializeField]
        ScriptableCardData cardData = null;

        protected override void StartAction(ICardPlayer cardPlayer)
        {
            bool isValidData = cardData != null;
            if (isValidData)
            {
                ICardData newCardData = (cardData as ICardData).Copy();
                cardPlayer.AddCardToDiscardDeck(newCardData);
            }        
        }

        protected override ICardAction Copy()
        {
            return new AddCard
            {
                DisplayName = DisplayName,
                Description = Description
            };
        }
    }
}

