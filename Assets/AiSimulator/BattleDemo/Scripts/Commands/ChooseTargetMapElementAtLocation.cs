using IndieDevTools.Agents;
using IndieDevTools.Traits;
using IndieDevTools.Commands;
using IndieDevTools.Maps;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Demo.BattleSimulator
{
    public class ChooseTargetMapElmentAtLocation : AbstractCommand
    {
        IAgent agent = null;

        protected override void OnStart()
        {
            SetTargetAgent();
            Complete();
        }

        void SetTargetAgent()
        {
            agent.TargetMapElement = GetHighestRankedMapElement();
        }

        IMapElement GetHighestRankedMapElement()
        {
            int highestEnemyRank = 0;
            int highestItemRank = 0;
            IMapElement highestRankedMapElement = null;

            List<IMapElement> mapElements = agent.Map.GetMapElementsAtCell<IMapElement>(agent.Location);
            foreach (IMapElement mapElement in mapElements)
            {
                if (mapElement == agent) continue;

                bool isAttackable = GetIsAttackable(mapElement);
                if (isAttackable)
                {
                    bool isEnemy = GetIsEnemy(mapElement);
                    if (isEnemy)
                    {
                        int enemyRank = GetEnemyRank(mapElement);
                        if (enemyRank > highestEnemyRank)
                        {
                            highestEnemyRank = enemyRank;
                            highestRankedMapElement = mapElement;
                        }
                    }
                }
                else if (highestEnemyRank == 0)
                {
                    int itemRank = GetItemRank(mapElement);
                    if (itemRank > highestItemRank)
                    {
                        highestItemRank = itemRank;
                        highestRankedMapElement = mapElement;
                    }
                }
            }

            return highestRankedMapElement;
        }

        bool GetIsAttackable (IMapElement mapElement)
        {
            return mapElement is IAttackReceiver;
        }

        bool GetIsEnemy(IMapElement mapElement)
        {
            return mapElement.GroupId != agent.GroupId;
        }

        int GetEnemyRank(IMapElement agentElement)
        {
            return 1000;
        }

        int GetItemRank(IMapElement itemElement)
        {
            List<ITrait> desires = agent.Desires;
            List<ITrait> stats = itemElement.Stats;

            int rank = 0;
            foreach (ITrait stat in stats)
            {
                foreach (ITrait desire in desires)
                {
                    if (stat.Id == desire.Id)
                    {
                        int attributeRank = stat.Quantity * desire.Quantity;
                        if (attributeRank >= rank)
                        {
                            rank = attributeRank;
                        }
                    }
                }
            }
            return rank;
        }

        public static ICommand Create(IAgent agent)
        {
            return new ChooseTargetMapElmentAtLocation
            {
                agent = agent
            };
        }
    }
}
