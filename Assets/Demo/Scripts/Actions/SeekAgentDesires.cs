using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Actions;
using RCG.Agents;

namespace RCG.Demo.Simulator
{
    public class SeekAgentDesires : AbstractAction
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

        IEnumerator CheckAdvertisements()
        {
            yield return null;
        }

        public static IAction Create(AbstractAgent agent)
        {
            SeekAgentDesires action = new SeekAgentDesires
            {
                agent = agent
            };

            return action;
        }
    }
}

