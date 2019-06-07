using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloppyKnights.Cards
{
    [CreateAssetMenu(fileName = "CardDataCollection", menuName = "Floppy Knights/Card Data Collection")]
    public class ScriptableCardDataCollection : ScriptableObject, ICardDataCollection
    {
        [SerializeField]
        List<ScriptableCardData> cardDatas = new List<ScriptableCardData>();
        List<ICardData> ICardDataCollection.CardDatas => CardDatas;
        List<ICardData> CardDatas
        {
            get
            {
                List<ICardData> iCardDatas = new List<ICardData>();
                foreach(ICardData cardData in cardDatas)
                {
                    iCardDatas.Add(cardData.Copy());
                }
                return iCardDatas;
            }
        }

        int ICardDataCollection.Count
        {
            get
            {
                return cardDatas.Count;
            }
        }

        ICardDataCollection ICardDataCollection.Copy()
        {
            return CardDataCollection.Create(CardDatas);
        }

        void ICardDataCollection.AddCard(ICardData cardData) { }
        void ICardDataCollection.AddCards(List<ICardData> cardDatas) { }
        void ICardDataCollection.RemoveCard(ICardData cardData) { }
        void ICardDataCollection.RemoveCards(List<ICardData> cardDatas) { }
        void ICardDataCollection.Clear() { }
        bool ICardDataCollection.Contains(ICardData cardData) => false;
        void ICardDataCollection.Shuffle() { }
        void ICardDataCollection.MoveCardTo(ICardData cardData, ICardDataCollection cardDataCollection) { }
        void ICardDataCollection.MoveAllCardsTo(ICardDataCollection cardDataCollection) { }
    }
}
