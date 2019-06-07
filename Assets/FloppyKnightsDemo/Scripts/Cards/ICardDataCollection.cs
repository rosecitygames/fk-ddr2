using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloppyKnights.Cards
{
    public interface ICardDataCollection
    {
        List<ICardData> CardDatas { get; }
        int Count { get; }
        void AddCard(ICardData cardData);
        void AddCards(List<ICardData> cardDatas);
        void RemoveCard(ICardData cardData);
        void RemoveCards(List<ICardData> cardDatas);
        void Clear();
        bool Contains(ICardData cardData);
        void Shuffle();
        void MoveCardTo(ICardData cardData, ICardDataCollection cardDataCollection);
        void MoveAllCardsTo(ICardDataCollection cardDataCollection);
        ICardDataCollection Copy();
    }
}
