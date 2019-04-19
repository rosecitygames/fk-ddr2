using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.States;
using RCG.Advertisements;

namespace RCG.Agents
{
    public class AgentState : ActionableState, IAdvertisementHandler
    {
        Agent agent = null;

        void IAdvertisementHandler.HandleAdvertisement(IAdvertisement advertisement)
        {

        }

        public static AgentState Create(string name, Agent agent)
        {
            AgentState state = new AgentState
            {
                stateName = name,
                agent = agent
            };
            return state;
        }
    }
}
