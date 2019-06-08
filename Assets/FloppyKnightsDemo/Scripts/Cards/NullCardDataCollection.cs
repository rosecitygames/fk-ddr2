using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;

namespace FloppyKnights.Cards
{
    public class NullCardDataCollection : ICardDataCollection
    {
        string IDescribable.DisplayName => "";
        string IDescribable.Description => "";
        List<ICardData> ICardDataCollection.CardDatas => new List<ICardData>();
        int ICardDataCollection.Count => 0;
        void ICardDataCollection.AddCard(ICardData cardData) { }
        void ICardDataCollection.AddCards(List<ICardData> cardDatas) { }
        void ICardDataCollection.RemoveCard(ICardData cardData) { }
        void ICardDataCollection.RemoveCards(List<ICardData> cardDatas) { }
        void ICardDataCollection.Clear() { }
        bool ICardDataCollection.HasCard(ICardData cardData) => false;
        bool ICardDataCollection.HasCard(string cardId) => false;
        ICardData ICardDataCollection.GetCard(string cardId) => null;
        bool ICardDataCollection.HasCardWithAction(string cardActionId) => false;
        void ICardDataCollection.Shuffle() { }
        void ICardDataCollection.MoveCardTo(ICardData cardData, ICardDataCollection cardDataCollection) { }
        void ICardDataCollection.MoveAllCardsTo(ICardDataCollection cardDataCollection) { }
        ICardDataCollection ICardDataCollection.Copy() => Create();

        public static ICardDataCollection Create()
        {
            return new NullCardDataCollection();
        }
    }
}
