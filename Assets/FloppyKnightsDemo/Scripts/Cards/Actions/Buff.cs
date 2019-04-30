using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Agents;
using RCG.Attributes;
using FloppyKnights.Agents;

namespace FloppyKnights.Cards
{
    [System.Serializable]
    public class Buff : AbstractCardAction
    {
        [SerializeField]
        AttributeCollection buffs = new AttributeCollection();

        ICardAgent targetAgent;

        protected override void StartAction(ICardPlayer cardPlayer)
        {
            bool isValidPlayer = cardPlayer != null && cardPlayer.TargetAgent != null && cardPlayer.TargetLocation != null;
            if (isValidPlayer)
            {
                targetAgent = cardPlayer.TargetAgent;
                targetAgent.OnIdleStarted += TargetAgent_OnIdleStarted;
                targetAgent.Buff(buffs);
            }
        }

        void TargetAgent_OnIdleStarted()
        {
            targetAgent.OnIdleStarted -= TargetAgent_OnIdleStarted;
            CallActionCompleted();
        }

        protected override ICardAction Copy()
        {
            return new Buff
            {
                DisplayName = DisplayName,
                Description = Description,
                buffs = buffs
            };
        }
    }
}

