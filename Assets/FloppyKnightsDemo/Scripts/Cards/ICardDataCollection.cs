using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloppyKnights.Cards
{
    public interface ICardDataCollection
    {
        List<ICardData> CardDatas { get; }
        void AddCard(ICardData cardData);
        void RemoveCard(ICardData cardData);
        void Clear();
        void Shuffle();
    }
}
