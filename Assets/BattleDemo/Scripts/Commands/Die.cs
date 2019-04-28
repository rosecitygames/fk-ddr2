using RCG.Agents;
using RCG.Commands;
using UnityEngine;

namespace RCG.Demo.BattleSimulator
{
    public class Die : AbstractCommand
    {
        IAgent agent = null;

        protected override void OnStart()
        {
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
