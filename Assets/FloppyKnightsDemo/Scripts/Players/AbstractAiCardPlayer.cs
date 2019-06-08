using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FloppyKnights.Agents;
using FloppyKnights.Cards;
using RCG.Attributes;

namespace FloppyKnights.CardPlayers
{
    [RequireComponent(typeof(ICardAgent))]
    public abstract class AbstractAiCardPlayer : AbstractCardPlayer, ICardPlayer
    {
        ICardAgent cardAgent = null;
        
        protected ICardAgent CardAgent
        {
            get
            {
                if (cardAgent == null)
                {
                    cardAgent = GetComponent<ICardAgent>();
                }
                return cardAgent;
            }           
        }

        protected override string DisplayName => CardAgent.DisplayName;
        protected override string Description => CardAgent.Description;

        [SerializeField]
        int groupId;
        protected override int GroupId { get => groupId; set => groupId = value; }

        [SerializeField]
        int initialBaseEnergy = 0;
        protected override int BaseEnergy => initialBaseEnergy;

        [SerializeField]
        ScriptableCardDataCollection initialBaseDeck = null;

        protected override void InitDecks()
        {
            if (initialBaseDeck != null)
            {
                BaseDeck = CardDataCollection.Create(initialBaseDeck.CardDatas, "Base");
            }
            else
            {
                BaseDeck = NullCardDataCollection.Create();
            }

            HandDeck = CardDataCollection.Create("Hand");
            DiscardDeck = CardDataCollection.Create("Discard");
        }

        protected override void EndTurn()
        {
            HandDeck.MoveAllCardsTo(DiscardDeck);
            DiscardDeck.MoveAllCardsTo(BaseDeck);
            CallOnTurnCompleted();
        }

        [ContextMenu("Start Turn")]
        void TestStartTurn()
        {
            StarTurn();
        }
    }
}

