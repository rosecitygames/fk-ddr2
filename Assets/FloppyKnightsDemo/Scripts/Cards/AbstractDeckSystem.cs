using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloppyKnights.Cards
{
    public abstract class AbstractDeckSystem : IDeckSystem
    {
        Dictionary<string, ICardDataCollection> decksByIds = new Dictionary<string, ICardDataCollection>();

        void IDeckSystem.AddDeck(string deckId) => AddDeck(deckId);
        protected void AddDeck(string deckId)
        {
            bool hasDeck = decksByIds.ContainsKey(deckId);
            if (hasDeck)
            {
                Debug.LogWarning("Deck already exists in system : " + deckId);
                return;
            }

            ICardDataCollection deck = CardDataCollection.Create();
            decksByIds.Add(deckId, deck);
        }

        void IDeckSystem.AddDeck(string deckId, ICardDataCollection deck) => AddDeck(deckId, deck);
        protected void AddDeck(string deckId, ICardDataCollection deck)
        {
            bool hasDeck = decksByIds.ContainsKey(deckId) || decksByIds.ContainsValue(deck);
            if (hasDeck)
            {
                Debug.LogWarning("Deck already exists in system : " + deckId);
                return;
            }

            decksByIds.Add(deckId, deck);
        }

        void IDeckSystem.RemoveDeck(string deckId) => RemoveDeck(deckId);
        protected void RemoveDeck(string deckId)
        {
            bool hasId = decksByIds.ContainsKey(deckId);
            if (hasId)
            {
                decksByIds.Remove(deckId);
            }
        }

        ICardDataCollection IDeckSystem.GetDeck(string deckId) => GetDeck(deckId);
        protected ICardDataCollection GetDeck(string deckId)
        {
            bool hasId = decksByIds.ContainsKey(deckId);
            if (hasId)
            {
                return decksByIds[deckId];
            }
            else
            {
                return NullCardDataCollection.Create();
            }
        }

        int IDeckSystem.GetDeckCount(string deckId) => GetDeckCount(deckId);
        protected int GetDeckCount(string deckId)
        {
            ICardDataCollection deck = GetDeck(deckId);
            return deck.Count;
        }

        void IDeckSystem.AddCardToDeck(string deckId, ICardData cardData) => AddCardToDeck(deckId, cardData);
        protected void AddCardToDeck(string deckId, ICardData cardData)
        {
            ICardDataCollection deck = GetDeck(deckId);
            deck.AddCard(cardData);
        }

        void IDeckSystem.AddCardsToDeck(string deckId, List<ICardData> cardDatas) => AddCardsToDeck(deckId, cardDatas);
        protected void AddCardsToDeck(string deckId, List<ICardData> cardDatas)
        {
            ICardDataCollection deck = GetDeck(deckId);
            deck.AddCards(cardDatas);
        }

        void IDeckSystem.RemoveCardFromDeck(string deckId, ICardData cardData) => RemoveCardFromDeck(deckId, cardData);
        protected void RemoveCardFromDeck(string deckId, ICardData cardData)
        {
            ICardDataCollection deck = GetDeck(deckId);
            deck.RemoveCard(cardData);
        }

        void IDeckSystem.RemoveCardsFromDeck(string deckId, List<ICardData> cardDatas) => RemoveCardsFromDeck(deckId, cardDatas);
        protected void RemoveCardsFromDeck(string deckId, List<ICardData> cardDatas)
        {
            ICardDataCollection deck = GetDeck(deckId);
            deck.RemoveCards(cardDatas);
        }

        void IDeckSystem.ClearDeck(string deckId) => ClearDeck(deckId);
        protected void ClearDeck(string deckId)
        {
            ICardDataCollection deck = GetDeck(deckId);
            deck.Clear();
        }

        bool IDeckSystem.DeckContains(string deckId, ICardData cardData) => DeckContains(deckId, cardData);
        protected bool DeckContains(string deckId, ICardData cardData)
        {
            ICardDataCollection deck = GetDeck(deckId);
            return deck.Contains(cardData);
        }

        void IDeckSystem.ShuffleDeck(string deckId) => ShuffleDeck(deckId);
        protected void ShuffleDeck(string deckId)
        {
            ICardDataCollection deck = GetDeck(deckId);
            deck.Shuffle();
        }

        void IDeckSystem.MoveCardFromTo(ICardData cardData, string fromDeckId, string toDeckId) => MoveCardFromTo(cardData, fromDeckId, toDeckId);
        protected void MoveCardFromTo(ICardData cardData, string fromDeckId, string toDeckId)
        {
            ICardDataCollection fromDeck = GetDeck(fromDeckId);
            ICardDataCollection toDeck = GetDeck(toDeckId);
            fromDeck.MoveCardTo(cardData, toDeck);
        }

        void IDeckSystem.MoveAllCardsFromTo(string fromDeckId, string toDeckId) => MoveAllCardsFromTo(fromDeckId, toDeckId);
        protected void MoveAllCardsFromTo(string fromDeckId, string toDeckId)
        {
            ICardDataCollection fromDeck = GetDeck(fromDeckId);
            ICardDataCollection toDeck = GetDeck(toDeckId);
            fromDeck.MoveAllCardsTo(toDeck);
        }
    }
}
