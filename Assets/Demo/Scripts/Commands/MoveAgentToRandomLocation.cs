using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Commands;
using RCG.Agents;

namespace RCG.Demo.Simulator
{
    public class MoveAgentToRandomLocation : AbstractCommand
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

        public static ICommand Create(AbstractAgent agent)
        {
            MoveAgentToRandomLocation command = new MoveAgentToRandomLocation
            {
                agent = agent
            };

            return command;
        }
    }
}

