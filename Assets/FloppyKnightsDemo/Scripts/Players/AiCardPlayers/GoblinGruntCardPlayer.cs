using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FloppyKnights.Cards;

namespace FloppyKnights.CardPlayers
{
    public class GoblinGruntCardPlayer : AbstractAiCardPlayer
    {
        ICardData currentCardData = null;
        int currentCardActionIndex = -1;

        protected override void StarTurn()
        {
            ResetEnergy();
            BaseDeck.MoveAllCardsTo(HandDeck);
            PlayCardFromHandDeck();
        }

        void PlayCardFromHandDeck()
        {
            RemoveCurrentCardDataEventHandlers();
            HandDeck.MoveCardTo(currentCardData, DiscardDeck);
            currentCardData = null;

            if (HandDeck.HasCard("Move1"))
            {
                ICardData moveCard = HandDeck.GetCard("Move1");
                bool hasEnergyToPlayCard = GetHasEnergyToPlayCard(moveCard);
                if (hasEnergyToPlayCard)
                {
                    currentCardData = moveCard;
                    ChooseMoveCardTargets();
                    PlayCurrentCard();
                }
            }
            
            if (currentCardData == null)
            {
                EndTurn();
            }     
        }

        bool GetHasEnergyToPlayCard(ICardData cardData)
        {
            return Energy >= cardData.Cost;
        }

        void PlayCurrentCard()
        {
            Energy -= currentCardData.Cost;
            
            bool hasCardActions = currentCardData.CardActions.Count > 0;
            if (hasCardActions)
            {
                AddCurrentCardDataEventHandlers();
                currentCardActionIndex = -1;
                PlayNextCardAction();
            }
            else
            {
                PlayCardFromHandDeck();
            }
        }

        void PlayNextCardAction()
        {
            bool hasRemainingCardActions = currentCardActionIndex < currentCardData.CardActions.Count -1;
            if (hasRemainingCardActions)
            {
                currentCardActionIndex += 1;
                ICardAction cardAction = currentCardData.CardActions[currentCardActionIndex];
                cardAction.StartAction(this);
            }
            else
            {
                RemoveCardAgentEventHandlers();
                PlayCardFromHandDeck();        
            }
        }

        void ChooseMoveCardTargets()
        {
            // Select target agent and/or location
            TargetAgent = CardAgent;
            TargetLocation = GetNewLocation(1);

            // If valid target and about to call on agent to do something...
            AddCardAgentEventHandlers();
        }

        void AddCurrentCardDataEventHandlers()
        {
            if (currentCardData == null) return;

            RemoveCurrentCardDataEventHandlers();
            
            foreach (ICardAction cardAction in currentCardData.CardActions)
            {
                cardAction.OnActionCompleted += CardAction_OnActionCompleted;
            }
        }

        void RemoveCurrentCardDataEventHandlers()
        {
            if (currentCardData == null) return;
            foreach(ICardAction cardAction in currentCardData.CardActions)
            {
                cardAction.OnActionCompleted -= CardAction_OnActionCompleted;
            }
        }

        private void CardAction_OnActionCompleted(ICardAction obj)
        {
            PlayNextCardAction();
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

