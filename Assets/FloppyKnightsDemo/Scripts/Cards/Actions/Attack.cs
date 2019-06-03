using FloppyKnights.Agents;
using FloppyKnights.CardPlayers;
using UnityEngine;

namespace FloppyKnights.Cards
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Floppy Knights/Actions/Attack")]
    public class Attack :ScriptableCardAction
    {
        [SerializeField]
        AttackAction cardAction = new AttackAction();

        protected override ICardAction GetCardAction()
        {
            return cardAction as ICardAction;
        }
    }

    [System.Serializable]
    public class AttackAction : AbstractCardAction
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
            return new AttackAction
            {
                DisplayName = DisplayName,
                Description = Description
            };
        }
    }
}

