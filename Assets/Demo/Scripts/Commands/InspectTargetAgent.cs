using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using RCG.Maps;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class InspectTargetAgent : AbstractCommand
    {
        IAgent agent = null;

        string enemyFoundTransition = "";
        string itemFoundTransition = "";
        string nothingFoundTransition = "";
        

        protected override void OnStart()
        {
            agent.TargetAdvertisement = null;
            HandleMapCellMapElements();
            Complete();
        }

        void HandleMapCellMapElements()
        {
            IAgent targetAgent = agent.TargetAgent;

            bool isNotNullAgent = targetAgent != null;
            if (isNotNullAgent)
            {
                bool isEnemy = GetIsEnemy(targetAgent);
                if (isEnemy)
                {
                    agent.HandleTransition(enemyFoundTransition);
                }
                else
                {
                    agent.HandleTransition(itemFoundTransition);
                }
            }
            else
            {
                agent.HandleTransition(nothingFoundTransition);
            }
        }

        bool GetIsEnemy(IAgent otherAgent)
        {
            return (otherAgent.GroupId != 0 && otherAgent.GroupId != agent.GroupId);
        }

        const string healthAttributeId = "health";
        bool GetIsItem(IAgent otherAgent)
        {
            IAttribute healthAttribute = otherAgent.GetStat(healthAttributeId);
            return (healthAttribute == null || healthAttribute.Quantity == 0);
        }

        public static ICommand Create(IAgent agent, string handleEnemeyTransition = "", string handleItemTransition = "", string handleNothingTransition = "")
        {
            return new InspectTargetAgent
            {
                agent = agent,
                enemyFoundTransition = handleEnemeyTransition,
                itemFoundTransition = handleItemTransition,
                nothingFoundTransition = handleNothingTransition
            };
        }
    }
}
