using System.Collections.Generic;
using UnityEngine;
using RCG.Advertisements;
using RCG.Agents;
using RCG.States;

namespace RCG.Demo.Simulator
{
    public class Agent : AbstractAgent
    {
        protected override void InitStateMachine()
        {
            CommandableState wanderState = CommandableState.Create("wander");
            wanderState.AddCommand(MoveAgentToRandomLocation.Create(this));
            wanderState.AddCommand(DefaultAdvertisementHandler.Create(this));
            stateMachine.AddState(wanderState);
        }
    }
}
