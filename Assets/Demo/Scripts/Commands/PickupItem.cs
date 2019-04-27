﻿using RCG.Agents;
using RCG.Commands;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class PickupItem : AbstractCommand
    {
        IAgent agent = null;

        protected override void OnStart()
        {
            if (agent.TargetMapElement != null)
            {
                Debug.Log("Picking up " + agent.DisplayName);
                agent.TargetMapElement.RemoveFromMap();
                agent.TargetMapElement = null;
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
