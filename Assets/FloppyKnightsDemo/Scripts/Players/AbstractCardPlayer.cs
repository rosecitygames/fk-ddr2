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

        void ICardPlayer.AddCardToDiscardDeck(ICardData cardData) => AddCardToDiscardDeck(cardData);
        protected virtual void AddCardToDiscardDeck(ICardData cardData) { }
    }
}

