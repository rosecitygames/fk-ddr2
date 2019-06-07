using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloppyKnights.Cards
{
    public interface IDeckSystem
    {
        void AddDeck(string deckId);
        void AddDeck(string deckId, ICardDataCollection deck);
        void RemoveDeck(string deckId);
        ICardDataCollection GetDeck(string deckId);
        int GetDeckCount(string deckId);
        void AddCardToDeck(string deckId, ICardData cardData);
        void AddCardsToDeck(string deckId, List<ICardData> cardDatas);
        void RemoveCardFromDeck(string deckId, ICardData cardData);
        void RemoveCardsFromDeck(string deckId, List<ICardData> cardDatas);
        void ClearDeck(string deckId);
        bool DeckContains(string deckId, ICardData cardData);
        void ShuffleDeck(string deckId);
        void MoveCardFromTo(ICardData cardData, string fromDeckId, string toDeckId);
        void MoveAllCardsFromTo(string fromDeckId, string toDeckId);
    }
}
