using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Actions;
using RCG.Agents;

namespace RCG.Demo.Simulator
{
    public class MoveAgentToRandomLocation : AbstractAction
    {
        AbstractAgent agent;

        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
            
        }

        protected override void OnDestroy()
        {
            
        }

        IEnumerator Move()
        {
            yield return null;
        }

        public static IAction Create(AbstractAgent agent)
        {
            MoveAgentToRandomLocation action = new MoveAgentToRandomLocation
            {
                agent = agent
            };

            return action;
        }
    }
}

