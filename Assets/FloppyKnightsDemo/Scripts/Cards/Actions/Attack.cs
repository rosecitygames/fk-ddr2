using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FloppyKnights.Agents;

namespace FloppyKnights.Cards
{
    [System.Serializable]
    public class Attack : AbstractCardAction
    {
        ICardAgent targetAgent;

        protected override void StartAction(ICardPlayer cardPlayer)
        {
            bool isValidPlayer = cardPlayer != null && cardPlayer.TargetAgent != null && cardPlayer.TargetLocation != null;
            if (isValidPlayer)
            {
                targetAgent = cardPlayer.TargetAgent;
                targetAgent.OnIdleStarted += TargetAgent_OnIdleStarted;
                targetAgent.Attack(cardPlayer.TargetAgent);
            }
        }

        void TargetAgent_OnIdleStarted()
        {
            targetAgent.OnIdleStarted -= TargetAgent_OnIdleStarted;
            CallActionCompleted();
        }

        protected override ICardAction Copy()
        {
            return new Attack
            {
                DisplayName = DisplayName,
                Description = Description
            };
        }
    }
}

