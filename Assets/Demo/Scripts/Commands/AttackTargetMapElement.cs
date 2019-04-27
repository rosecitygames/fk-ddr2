using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using RCG.Utils;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class AttackTargetMapElement : AbstractCommand
    {
        IAgent agent = null;
        string onTargetKilledTransition;

        protected override void OnStart()
        {
            AttackTarget();
            Complete();
        }

        void AttackTarget()
        {
            if (agent.TargetMapElement == null)
            {
                CallTargetDeathTransition();
                return;
            }

            IAttackReceiver attackReceiver = agent.TargetMapElement as IAttackReceiver;
            if (attackReceiver == null)
            {
                CallTargetDeathTransition();
                return;
            }

            int targetHealth = AttributesUtil.GetHealth(agent.TargetMapElement);
            if (targetHealth <= 0)
            {
                CallTargetDeathTransition();
            }
            else
            {
                attackReceiver.ReceiveAttack(agent);
            }        
        }

        void CallTargetDeathTransition()
        {
            if (string.IsNullOrEmpty(onTargetKilledTransition) == false)
            {
                agent.HandleTransition(onTargetKilledTransition);
            }        
        }

        public static ICommand Create(IAgent agent, string onTargetKilledTransition)
        {
            return new AttackTargetMapElement
            {
                agent = agent,
                onTargetKilledTransition = onTargetKilledTransition
            };
        }
    }
}
