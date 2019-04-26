using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using RCG.Maps;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class InspectTargetMapElement : AbstractCommand
    {
        IAgent agent = null;

        string enemyFoundTransition = "";
        string itemFoundTransition = "";
        string nothingFoundTransition = "";
        

        protected override void OnStart()
        {
            agent.TargetAdvertisement = null;
            Inspect();
            Complete();
        }

        void Inspect()
        {
            IMapElement targetMapElement = agent.TargetMapElement;

            bool isNotNullMapElement = targetMapElement != null;
            if (isNotNullMapElement)
            {
                bool isEnemy = GetIsEnemy(targetMapElement);
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

        bool GetIsEnemy(IMapElement mapElement)
        {
            bool isAttackable = (mapElement as IAttackReceiver) != null;
            return (isAttackable && mapElement.GroupId != agent.GroupId);
        }

        public static ICommand Create(IAgent agent, string handleEnemeyTransition = "", string handleItemTransition = "", string handleNothingTransition = "")
        {
            return new InspectTargetMapElement
            {
                agent = agent,
                enemyFoundTransition = handleEnemeyTransition,
                itemFoundTransition = handleItemTransition,
                nothingFoundTransition = handleNothingTransition
            };
        }
    }
}
