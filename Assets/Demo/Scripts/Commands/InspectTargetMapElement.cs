using RCG.Agents;
using RCG.Commands;
using RCG.Items;
using RCG.Maps;
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
            bool isNotSelf = targetMapElement != agent;
            if (isNotNullMapElement && isNotSelf)
            {
                bool isEnemy = GetIsEnemy(targetMapElement);
                if (isEnemy)
                {
                    Debug.Log(agent + " found enemy " + targetMapElement);
                    agent.HandleTransition(enemyFoundTransition);
                }
                else
                {
                    bool isItem = GetIsItem(targetMapElement);
                    if (isItem)
                    {
                        Debug.Log(agent + " found some " + targetMapElement);
                        agent.HandleTransition(itemFoundTransition);
                    }
                    else
                    {
                        agent.HandleTransition(nothingFoundTransition);
                    }
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

        bool GetIsItem(IMapElement mapElement)
        {
            return (mapElement as IItem) != null;
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
