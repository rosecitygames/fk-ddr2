using IndieDevTools.Agents;
using IndieDevTools.Commands;
using UnityEngine;

namespace IndieDevTools.Demo.BattleSimulator
{
    public class PickupItem : AbstractCommand
    {
        IAgent agent = null;

        protected override void OnStart()
        {
            if (agent.TargetMapElement != null)
            {
                agent.TargetMapElement.Description = "Picked up by " + agent.DisplayName;
                agent.Description = "Picked up " + agent.TargetMapElement.DisplayName;
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
