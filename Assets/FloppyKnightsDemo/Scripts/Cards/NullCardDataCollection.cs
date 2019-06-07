using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloppyKnights.Cards
{
    public class NullCardDataCollection : ICardDataCollection
    {
        List<ICardData> ICardDataCollection.CardDatas => new List<ICardData>();
        int ICardDataCollection.Count => 0;
        void ICardDataCollection.AddCard(ICardData cardData) { }
        void ICardDataCollection.AddCards(List<ICardData> cardDatas) { }
        void ICardDataCollection.RemoveCard(ICardData cardData) { }
        void ICardDataCollection.RemoveCards(List<ICardData> cardDatas) { }
        void ICardDataCollection.Clear() { }
        bool ICardDataCollection.Contains(ICardData cardData) => false;
        void ICardDataCollection.Shuffle() { }
        void ICardDataCollection.MoveCardTo(ICardData cardData, ICardDataCollection cardDataCollection) { }
        void ICardDataCollection.MoveAllCardsTo(ICardDataCollection cardDataCollection) { }

        public static ICardDataCollection Create()
        {
            return new NullCardDataCollection();
        }
    }
}
