using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FloppyKnights.Agents;
using FloppyKnights.Cards;
using RCG.Attributes;

namespace FloppyKnights.Agents
{
    [RequireComponent(typeof(ICardAgent))]
    public class AbstractAiCardPlayer : MonoBehaviour, ICardPlayer
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

        string IDescribable.DisplayName
        {
            get
            {
                return CardAgent.DisplayName;
            }
        }

        string IDescribable.Description
        {
            get
            {
                return CardAgent.Description;
            }
        }

        [SerializeField]
        int groupId;

        int IGroupMember.GroupId
        {
            get
            {
                return GroupId;
            }
            set
            {
                GroupId = value;
            }         
        }

        int GroupId
        {
            get
            {
                return groupId;
            }

            set
            {
                groupId = value;
            }
        }

        void ITurnTaker.StartTurn()
        {
            StarTurn();
        }

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

        ICardAgent ICardPlayer.TargetAgent
        {
            get
            {
                return TargetAgent;
            }
        }

        protected ICardAgent TargetAgent { get; set; }

        Vector3Int ICardPlayer.TargetLocation
        {
            get
            {
                return TargetLocation;
            }
        }

        protected Vector3Int TargetLocation { get; set; }

        void ICardPlayer.AddCardToDiscardDeck(ICardData cardData)
        {

        }
    }
}

