using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using RCG.Maps;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class ChooseTargetAgentAtLocation : AbstractCommand
    {
        IAgent agent = null;

        protected override void OnStart()
        {
            SetTargetAgent();
            Complete();
        }

        void SetTargetAgent()
        {
            agent.TargetAgent = GetHighestRankedAgent();
        }

        IAgent GetHighestRankedAgent()
        {
            int highestEnemyRank = 0;
            int highestItemRank = 0;
            IAgent highestRankedAgent = null;

            List<IMapElement> mapElements = agent.Map.GetMapElementsAtCell(agent.Location);
            foreach (IMapElement mapElement in mapElements)
            {
                IAgent otherAgent = mapElement as IAgent;
                bool isAgent = otherAgent != null;
                if (isAgent)
                {
                    bool isEnemy = GetIsEnemy(otherAgent);
                    if (isEnemy)
                    {
                        int enemyRank = GetEnemyRank(otherAgent);
                        if (enemyRank > highestEnemyRank)
                        {
                            highestEnemyRank = enemyRank;
                            highestRankedAgent = otherAgent;
                        }
                    }
                    else if (highestEnemyRank == 0)
                    {
                        bool isItem = GetIsItem(otherAgent);
                        if (isItem)
                        {
                            int itemRank = GetItemRank(otherAgent);
                            if (itemRank > highestItemRank)
                            {
                                highestItemRank = itemRank;
                                highestRankedAgent = otherAgent;
                            }
                        }
                    }
                }
            }

            return highestRankedAgent;
        }

        bool GetIsEnemy(IAgent otherAgent)
        {
            bool isAttackable = (otherAgent as IAttackReceiver) != null;
            return (isAttackable && otherAgent.GroupId != 0 && otherAgent.GroupId != agent.GroupId);
        }

        const string healthAttributeId = "health";
        bool GetIsItem(IAgent otherAgent)
        {
            IAttribute healthAttribute = otherAgent.GetStat(healthAttributeId);
            return (healthAttribute == null || healthAttribute.Quantity == 0);
        }

        int GetEnemyRank(IAgent otherAgent)
        {
            return 0;
        }

        int GetItemRank(IAgent itemElement)
        {
            List<IAttribute> desires = agent.Desires;
            List<IAttribute> stats = itemElement.Stats;

            int rank = 0;
            foreach (IAttribute stat in stats)
            {
                foreach (IAttribute desire in desires)
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
            return new ChooseTargetAgentAtLocation
            {
                agent = agent
            };
        }
    }
}
