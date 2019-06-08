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
            cardAction.Id = name;
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
                cardPlayer.DiscardDeck.AddCard(newCardData);
            }
        }

        protected override ICardAction Copy()
        {
            return new AddCardAction
            {
                Id = Id,
                DisplayName = DisplayName,
                Description = Description
            };
        }
    }
}
