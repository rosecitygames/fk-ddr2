using System.Collections.Generic;
using UnityEngine;
using RCG.Advertisements;
using RCG.Agents;
using RCG.States;

namespace RCG.Demo.Simulator
{
    public class Agent : AbstractAgent
    {
        const int CommandLayer0 = 0;
        const int CommandLayer1 = 1;

        protected override void InitStateMachine()
        {
            CommandableState wanderState = CommandableState.Create("wander");
            wanderState.AddCommand(MoveAgentToRandomLocation.Create(this), CommandLayer0);
            wanderState.AddCommand(DefaultAdvertisementHandler.Create(this), CommandLayer1);
            stateMachine.AddState(wanderState);
        }

        public static IAgent Create(GameObject gameObject, IAgentData agentData, IAdvertisementBroadcaster broadcaster)
        {
            IAgent agent = gameObject.AddComponent<Agent>();
            agent.AgentData = agentData;
            agent.SetBroadcaster(broadcaster);
            return agent;
        }
    }
}
