using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FloppyKnights.Agents;
using FloppyKnights.Cards;
using RCG.Attributes;

namespace FloppyKnights.CardPlayers
{
    public class AbstractCardPlayer : MonoBehaviour, ICardPlayer
    {
        string IDescribable.DisplayName => DisplayName;
        protected virtual string DisplayName { get; }

        string IDescribable.Description => Description;
        protected virtual string Description { get; }

        int IGroupMember.GroupId { get => GroupId; set => GroupId = value; }
        protected virtual int GroupId { get; set; }

        void ITurnTaker.StartTurn() => StarTurn();
        protected virtual void StarTurn() { }

        event Action<ITurnTaker> ITurnTaker.OnTurnCompleted
        {
            add
            {
                OnTurnCompleted += value;
            }
            remove
            {
                OnTurnCompleted -= value;
            }
        }

        protected Action<ITurnTaker> OnTurnCompleted;

        protected virtual void CallOnTurnCompleted()
        {
            OnTurnCompleted?.Invoke(this);
        }

        ICardAgent ICardPlayer.TargetAgent => TargetAgent;
        protected ICardAgent TargetAgent { get; set; }

        Vector3Int ICardPlayer.TargetLocation => TargetLocation;
        protected Vector3Int TargetLocation { get; set; }
        
        // Deck System

        IDeckSystem deckSystem = DeckSystem.Create();

        void IDeckSystem.AddDeck(string deckId) => AddDeck(deckId);
        protected void AddDeck(string deckId) => deckSystem.AddDeck(deckId);

        void IDeckSystem.AddDeck(string deckId, ICardDataCollection deck) => AddDeck(deckId, deck);
        protected void AddDeck(string deckId, ICardDataCollection deck) => deckSystem.AddDeck(deckId, deck);

        void IDeckSystem.RemoveDeck(string deckId) => RemoveDeck(deckId);
        protected void RemoveDeck(string deckId) => deckSystem.RemoveDeck(deckId);

        ICardDataCollection IDeckSystem.GetDeck(string deckId) => GetDeck(deckId);
        protected ICardDataCollection GetDeck(string deckId) => deckSystem.GetDeck(deckId);

        int IDeckSystem.GetDeckCount(string deckId) => GetDeckCount(deckId);
        protected int GetDeckCount(string deckId) => deckSystem.GetDeckCount(deckId);

        void IDeckSystem.AddCardToDeck(string deckId, ICardData cardData) => AddCardToDeck(deckId, cardData);
        protected void AddCardToDeck(string deckId, ICardData cardData) => deckSystem.AddCardToDeck(deckId, cardData);

        void IDeckSystem.AddCardsToDeck(string deckId, List<ICardData> cardDatas) => AddCardsToDeck(deckId, cardDatas);
        protected void AddCardsToDeck(string deckId, List<ICardData> cardDatas) => deckSystem.AddCardsToDeck(deckId, cardDatas);

        void IDeckSystem.RemoveCardFromDeck(string deckId, ICardData cardData) => RemoveCardFromDeck(deckId, cardData);
        protected void RemoveCardFromDeck(string deckId, ICardData cardData) => deckSystem.RemoveCardFromDeck(deckId, cardData);

        void IDeckSystem.RemoveCardsFromDeck(string deckId, List<ICardData> cardDatas) => RemoveCardsFromDeck(deckId, cardDatas);
        protected void RemoveCardsFromDeck(string deckId, List<ICardData> cardDatas) => deckSystem.RemoveCardsFromDeck(deckId, cardDatas);

        void IDeckSystem.ClearDeck(string deckId) => ClearDeck(deckId);
        protected void ClearDeck(string deckId) => deckSystem.ClearDeck(deckId);

        bool IDeckSystem.DeckContains(string deckId, ICardData cardData) => DeckContains(deckId, cardData);
        protected bool DeckContains(string deckId, ICardData cardData) => deckSystem.DeckContains(deckId, cardData);

        void IDeckSystem.ShuffleDeck(string deckId) => ShuffleDeck(deckId);
        protected void ShuffleDeck(string deckId) => deckSystem.ShuffleDeck(deckId);

        void IDeckSystem.MoveCardFromTo(ICardData cardData, string fromDeckId, string toDeckId) => MoveCardFromTo(cardData, fromDeckId, toDeckId);
        protected void MoveCardFromTo(ICardData cardData, string fromDeckId, string toDeckId) => deckSystem.MoveCardFromTo(cardData, fromDeckId, toDeckId);

        void IDeckSystem.MoveAllCardsFromTo(string fromDeckId, string toDeckId) => MoveAllCardsFromTo(fromDeckId, toDeckId);
        protected void MoveAllCardsFromTo(string fromDeckId, string toDeckId) => deckSystem.MoveAllCardsFromTo(fromDeckId, toDeckId);
    }
}

