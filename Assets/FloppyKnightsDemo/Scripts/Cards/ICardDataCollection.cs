using RCG.Attributes;
using System.Collections.Generic;

namespace FloppyKnights.Cards
{
    public interface ICardDataCollection : IDescribable
    {
        List<ICardData> CardDatas { get; }
        int Count { get; }
        void AddCard(ICardData cardData);
        void AddCards(List<ICardData> cardDatas);
        void RemoveCard(ICardData cardData);
        void RemoveCards(List<ICardData> cardDatas);
        void Clear();
        bool HasCard(ICardData cardData);
        bool HasCard(string cardId);
        ICardData GetCard(string cardId);
        void Shuffle();
        void MoveCardTo(ICardData cardData, ICardDataCollection cardDataCollection);
        void MoveAllCardsTo(ICardDataCollection cardDataCollection);
        ICardDataCollection Copy();
    }
}
