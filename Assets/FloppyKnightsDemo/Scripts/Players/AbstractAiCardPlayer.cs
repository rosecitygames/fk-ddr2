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
    public class AbstractAiCardPlayer : AbstractCardPlayer, ICardPlayer
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

    }
}

