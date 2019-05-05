using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FloppyKnights.Cards;

namespace FloppyKnights.CardPlayers
{
    public class GoblinGruntCardPlayer : AbstractAiCardPlayer
    {
        const string moveCardActionPath = "CardActions/Move1";
        ICardAction moveCardAction = null;
        ICardAction MoveCardAction
        {
            get
            {
                if (moveCardAction == null)
                {
                    ICardAction scriptableMoveCardAction = Resources.Load(moveCardActionPath) as ICardAction;
                    moveCardAction = scriptableMoveCardAction.Copy();
                }
                return moveCardAction;
            }
        }

        protected override void StarTurn()
        {
            // Examine map

            // Select target agent and/or location
            TargetAgent = CardAgent;
            TargetLocation = GetNewLocation(1);

            // If valid target and about to call on agent to do something...
            AddCardAgentEventHandlers();

            // Play a card
            MoveCardAction.StartAction(this);
            // or call on the agent directly (although less synergistic)
            // CardAgent.Move(TargetLocation);    
        }

        Vector3Int GetNewLocation(int moveRadius)
        {
            Vector3Int location = CardAgent.Location;

            Bounds mapBounds = new Bounds(Vector3.zero, CardAgent.Map.Size);
            bool isInBounds = false;

            while (isInBounds == false)
            {
                Vector2Int offset = Vector2Int.RoundToInt(Random.insideUnitCircle * moveRadius);
                location = CardAgent.Location;
                location.x += offset.x;
                location.y += offset.y;

                isInBounds = mapBounds.Contains(location);
            }

            return location;
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

