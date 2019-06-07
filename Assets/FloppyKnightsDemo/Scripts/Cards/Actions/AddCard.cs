using FloppyKnights.CardPlayers;
using UnityEngine;

namespace FloppyKnights.Cards
{
    [CreateAssetMenu(fileName = "AddCard", menuName = "Floppy Knights/Actions/Add Card")]
    public class AddCard : ScriptableCardAction
    {
        [SerializeField]
        AddCardAction cardAction = new AddCardAction();

        protected override ICardAction GetCardAction()
        {
            return cardAction as ICardAction;
        }
    }

    [System.Serializable]
    public class AddCardAction : AbstractCardAction
    {
        [SerializeField]
        ScriptableCardData cardData = null;

        protected override void StartAction(ICardPlayer cardPlayer)
        {
            bool isValidData = cardData != null;
            if (isValidData)
            {
                ICardData newCardData = (cardData as ICardData).Copy();
                cardPlayer.AddCardToDeck(DeckSystem.DiscardDeckId, newCardData);
            }
        }

        protected override ICardAction Copy()
        {
            return new AddCardAction
            {
                DisplayName = DisplayName,
                Description = Description
            };
        }
    }
}
