using RCG.Agents;
using RCG.Commands;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class Die : AbstractCommand
    {
        IAgent agent = null;

        protected override void OnStart()
        {
            Debug.Log(agent.DisplayName+" health = "+ AttributesUtil.GetHealth(agent));
            agent.RemoveFromMap();
            Complete();
        }

        public static ICommand Create(IAgent agent)
        {
            return new Die
            {
                agent = agent
            };
        }
    }
}
