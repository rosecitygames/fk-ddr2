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
            ActionableState wanderState = ActionableState.Create("wander");
            wanderState.AddAction(MoveAgentToRandomLocation.Create(this));
            stateMachine.AddState(wanderState);
        }

    }
}
