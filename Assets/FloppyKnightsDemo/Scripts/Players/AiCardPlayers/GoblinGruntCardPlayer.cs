using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloppyKnights.CardPlayers
{
    public class GoblinGruntCardPlayer : AbstractAiCardPlayer
    {
        protected override void StarTurn()
        {
            // Examine map

            // Select target agent and/or location

            // If valid target and about to call on agent to do something...
            AddCardAgentEventHandlers();

            // Tell your agent to do something
            CardAgent.Move(TargetLocation);            
        }

        void AddCardAgentEventHandlers()
        {
            CardAgent.OnIdleStarted += CardAgent_OnIdleStarted;
        }

        void RemoveCardAgentEventHandlers()
        {
            CardAgent.OnIdleStarted -= CardAgent_OnIdleStarted;
        }

        private void CardAgent_OnIdleStarted()
        {
            RemoveCardAgentEventHandlers();
            CallOnTurnCompleted();
        }
    }
}

