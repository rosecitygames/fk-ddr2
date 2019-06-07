using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloppyKnights.Cards
{
    public class CardDataCollection : ICardDataCollection
    {
        List<ICardData> ICardDataCollection.CardDatas => new List<ICardData>(cardDatas);
        List<ICardData> cardDatas = new List<ICardData>();

        int ICardDataCollection.Count => Count;
        int Count
        {
            get
            {
                return cardDatas.Count;
            }
        }

        void ICardDataCollection.AddCard(ICardData cardData) => AddCard(cardData);
        void AddCard(ICardData cardData)
        {
            cardDatas.Add(cardData);
        }
        void ICardDataCollection.AddCards(List<ICardData> cardDatas) => AddCards(cardDatas);
        void AddCards(List<ICardData> cardDatas)
        {
            cardDatas.AddRange(cardDatas);
        }

        void ICardDataCollection.RemoveCard(ICardData cardData) => RemoveCard(cardData);
        void RemoveCard(ICardData cardData)
        {
            if (cardDatas.Contains(cardData))
            {
                cardDatas.Remove(cardData);
            }
        }

        void ICardDataCollection.RemoveCards(List<ICardData> removeCardDatas) => RemoveCards(removeCardDatas);
        void RemoveCards(List<ICardData> removeCardDatas)
        {
            foreach (ICardData cardData in removeCardDatas)
            {
                RemoveCard(cardData);
            }
        }

        void ICardDataCollection.Clear() => Clear();
        void Clear()
        {
            cardDatas.Clear();
        }

        bool ICardDataCollection.Contains(ICardData cardData) => Contains(cardData);
        bool Contains(ICardData cardData)
        {
            return cardDatas.Contains(cardData);
        }

        void ICardDataCollection.Shuffle() => Shuffle();
        void Shuffle()
        {
            var count = Count;
            var lastIndex = count - 1;
            for (int i = 0; i < lastIndex; ++i)
            {
                int randomIndex = Random.Range(i, count);
                ICardData cached = cardDatas[i];
                cardDatas[i] = cardDatas[randomIndex];
                cardDatas[randomIndex] = cached;
            }
        }

        void ICardDataCollection.MoveCardTo(ICardData cardData, ICardDataCollection cardDataCollection) => MoveCardTo(cardData, cardDataCollection);
        void MoveCardTo(ICardData cardData, ICardDataCollection cardDataCollection)
        {
            if (Contains(cardData))
            {
                RemoveCard(cardData);
                cardDataCollection.AddCard(cardData);
            }
        }
        void ICardDataCollection.MoveAllCardsTo(ICardDataCollection cardDataCollection) => MoveAllCardsTo(cardDataCollection);
        void MoveAllCardsTo(ICardDataCollection cardDataCollection)
        {
            cardDataCollection.AddCards(cardDatas);
            Clear();
        }

        public static ICardDataCollection Create()
        {
            return new CardDataCollection();
        }

        public static ICardDataCollection Create(List<ICardData> cardDatas)
        {
            return new CardDataCollection
            {
                cardDatas = cardDatas
            };
        }
    }
}
