using FloppyKnights.Agents;
using FloppyKnights.CardPlayers;
using UnityEngine;

namespace FloppyKnights.Cards
{
    [CreateAssetMenu(fileName = "Move", menuName = "Floppy Knights/Actions/Move")]
    public class Move : ScriptableCardAction
    {
        [SerializeField]
        MoveAction cardAction = new MoveAction();

        protected override ICardAction GetCardAction()
        {
            return cardAction as ICardAction;
        }
    }

    [System.Serializable]
    public class MoveAction : AbstractCardAction
    {
        [SerializeField]
        int radius = 1;

        public int Radius
        {
            get
            {
                return radius;
            }
        }

        ICardAgent targetAgent;

        protected override void StartAction(ICardPlayer cardPlayer)
        {
            bool isValidPlayer = cardPlayer != null && cardPlayer.TargetAgent != null && cardPlayer.TargetLocation != null;
            if (isValidPlayer)
            {
                targetAgent = cardPlayer.TargetAgent;
                targetAgent.OnIdleStarted += TargetAgent_OnIdleStarted;
                targetAgent.Move(cardPlayer.TargetLocation);
            }
        }

        void TargetAgent_OnIdleStarted()
        {
            targetAgent.OnIdleStarted -= TargetAgent_OnIdleStarted;
            CallActionCompleted();
        }

        protected override ICardAction Copy()
        {
            return new MoveAction
            {
                DisplayName = DisplayName,
                Description = Description
            };
        }
    }
}

