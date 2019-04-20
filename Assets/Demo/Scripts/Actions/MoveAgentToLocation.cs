using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Actions;
using RCG.Agents;

namespace RCG.Demo.Simulator
{
    public class MoveAgentToLocation : AbstractAction
    {
        AbstractAgent agent = null;

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

        public static IAction Create(AbstractAgent agent, Vector2 location)
        {
            MoveAgentToLocation action = new MoveAgentToLocation
            {
                agent = agent
            };

            return action;
        }
    }
}

