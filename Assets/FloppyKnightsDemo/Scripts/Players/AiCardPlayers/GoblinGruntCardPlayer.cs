using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FloppyKnights.Cards;

namespace FloppyKnights.CardPlayers
{
    public class GoblinGruntCardPlayer : AbstractAiCardPlayer
    {
        const string move1CardDataPath = "CardDatas/Move1";
        ICardData move1CardData = null;
        ICardData Move1CardData
        {
            get
            {
                if (move1CardData == null)
                {
                    ICardData scriptableMove1CardData = Resources.Load(move1CardDataPath) as ICardData;
                    move1CardData = scriptableMove1CardData.Copy();
                }
                return move1CardData;
            }
        }

        ICardData currentCardData = null;
        int currentCardActionIndex = -1;

        protected override void StarTurn()
        {

            // Examine map

            // Choose a card from deck. in this case, move 1
            ICardData chosenCardData = Move1CardData;

            // Select target agent and/or location
            TargetAgent = CardAgent;
            TargetLocation = GetNewLocation(1);

            // If valid target and about to call on agent to do something...
            AddCardAgentEventHandlers();

            // Play a card
            PlayCard(chosenCardData);

            // or call on the agent directly (although less synergistic)
            // CardAgent.Move(TargetLocation);    
        }

        void PlayCard(ICardData cardData)
        {
            Debug.Log("Playing Card : " + cardData.DisplayName);

            RemoveCurrentCardDataEventHandlers();

            currentCardData = cardData;
            
            bool hasCardActions = currentCardData.CardActions.Count > 0;
            if (hasCardActions)
            {
                AddCurrentCardDataEventHandlers();
                currentCardActionIndex = -1;
                PlayNextCardAction();
            }
            else
            {
                // TODO : Play next card
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
                HandDeck.MoveCardTo(currentCardData, DiscardDeck);
                // TODO : Play next card
                CallOnTurnCompleted(); // For now
            }
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

