using RCG.Attributes;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace FloppyKnights.Cards
{
    public class CardDataCollection : ICardDataCollection
    {
        string IDescribable.DisplayName => DisplayName;
        protected string DisplayName { get; set; }

        string IDescribable.Description => Description;
        protected string Description { get; set; }

        List<ICardData> ICardDataCollection.CardDatas => new List<ICardData>(cardDatas);
        [ShowInInspector, ReadOnly]
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
        void ICardDataCollection.AddCards(List<ICardData> addCardDatas) => AddCards(addCardDatas);
        void AddCards(List<ICardData> addCardDatas)
        {
            cardDatas.AddRange(addCardDatas);
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

        bool ICardDataCollection.HasCard(ICardData cardData) => HasCard(cardData);
        bool HasCard(ICardData cardData)
        {
            return cardDatas.Contains(cardData);
        }

        bool ICardDataCollection.HasCard(string cardId) => HasCard(cardId);
        bool HasCard(string cardId)
        {
            foreach(ICardData cardData in cardDatas)
            {
                if (cardData.Id == cardId) return true;
            }
            return false;
        }

        ICardData ICardDataCollection.GetCard(string cardId) => GetCard(cardId);
        ICardData GetCard(string cardId)
        {
            foreach(ICardData cardData in cardDatas)
            {
                if (cardData.Id == cardId)
                {
                    return cardData;
                }
            }
            return null;
        }

        bool ICardDataCollection.HasCardWithAction(string cardActionId) => HasCardWithAction(cardActionId);
        bool HasCardWithAction(string cardActionId)
        {
            foreach(ICardData cardData in cardDatas)
            {            
                if (cardData.HasCardAction(cardActionId)) return true;
            }
            return false;
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
            if (HasCard(cardData))
            {
                cardDataCollection.AddCard(cardData);
                RemoveCard(cardData);
            }
        }

        void ICardDataCollection.MoveAllCardsTo(ICardDataCollection cardDataCollection) => MoveAllCardsTo(cardDataCollection);
        void MoveAllCardsTo(ICardDataCollection cardDataCollection)
        {
            cardDataCollection.AddCards(cardDatas);            
            Clear();
        }

        ICardDataCollection ICardDataCollection.Copy()
        {
            List<ICardData> copiedCardDatas = new List<ICardData>();
            foreach (ICardData cardData in cardDatas)
            {
                copiedCardDatas.Add(cardData.Copy());
            }

            return Create(copiedCardDatas);
        }

        public static ICardDataCollection Create(string displayName = "", string description = "")
        {
            return new CardDataCollection
            {
                DisplayName = displayName,
                Description = description
            };
        }

        public static ICardDataCollection Create(List<ICardData> cardDatas, string displayName = "", string description = "")
        {
            return new CardDataCollection
            {
                cardDatas = cardDatas,
                DisplayName = displayName,
                Description = description
            };
        }
    }
}
