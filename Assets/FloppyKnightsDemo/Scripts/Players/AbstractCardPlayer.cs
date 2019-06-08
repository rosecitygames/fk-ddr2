using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FloppyKnights.Agents;
using FloppyKnights.Cards;
using RCG.Attributes;
using Sirenix.OdinInspector;

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

        protected virtual void EndTurn() { }

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

        ICardDataCollection ICardPlayer.BaseDeck => BaseDeck;
        [ShowInInspector, ReadOnly]
        protected ICardDataCollection BaseDeck { get; set; }

        ICardDataCollection ICardPlayer.HandDeck => HandDeck;
        [ShowInInspector, ReadOnly]
        protected ICardDataCollection HandDeck { get; set; }

        ICardDataCollection ICardPlayer.DiscardDeck => DiscardDeck;
        [ShowInInspector, ReadOnly]
        protected ICardDataCollection DiscardDeck { get; set; }

        protected virtual void InitDecks()
        {
            BaseDeck = CardDataCollection.Create("Base");
            HandDeck = CardDataCollection.Create("Hand");
            DiscardDeck = CardDataCollection.Create("Discard");
        }

        int ICardPlayer.Energy => Energy;
        [ShowInInspector, ReadOnly]
        protected virtual int Energy { get; set; }

        protected virtual int BaseEnergy { get; set; }

        protected virtual void ResetEnergy()
        {
            Energy = BaseEnergy;
        }


        void Awake()
        {
            Init();
        }

        void Init()
        {
            InitDecks();
        }
    }
}

