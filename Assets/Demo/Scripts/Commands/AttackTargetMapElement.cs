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
            agent.TargetAdvertisement = null;
            AttackTarget();
            Complete();
        }

        void AttackTarget()
        {
            if (agent.TargetMapElement == null)
            {
                CallTargetKilledTransition();
                return;
            }

            IAttackReceiver attackReceiver = agent.TargetMapElement as IAttackReceiver;
            if (attackReceiver == null)
            {
                CallTargetKilledTransition();
                return;
            }

            int targetHealth = AttributesUtil.GetHealth(agent.TargetMapElement);
            if (targetHealth <= 0)
            {
                CallTargetKilledTransition();
            }
            else
            {
                attackReceiver.ReceiveAttack(agent);
            }        
        }

        void CallTargetKilledTransition()
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
