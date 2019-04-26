using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using RCG.Maps;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class PickupItem : AbstractCommand
    {
        IAgent agent = null;

        protected override void OnStart()
        {
            if (agent.TargetAgent != null)
            {
                agent.TargetAgent.RemoveFromMap();
                agent.TargetAgent = null;
            }

            Complete();
        }

        public static ICommand Create(IAgent agent)
        {
            return new PickupItem
            {
                agent = agent
            };
        }
    }
}
