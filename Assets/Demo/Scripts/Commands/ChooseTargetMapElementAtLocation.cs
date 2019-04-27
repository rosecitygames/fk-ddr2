﻿using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using RCG.Maps;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Demo.Simulator
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

            List<IMapElement> mapElements = agent.Map.GetMapElementsAtCell(agent.Location);
            foreach (IMapElement mapElement in mapElements)
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

        bool GetIsEnemy(IMapElement mapElement)
        {
            bool isAttackable = (mapElement as IAttackReceiver) != null;
            return (isAttackable && mapElement.GroupId != agent.GroupId);
        }

        int GetEnemyRank(IMapElement agentElement)
        {
            return 1000;
        }

        int GetItemRank(IMapElement itemElement)
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
            return new ChooseTargetMapElmentAtLocation
            {
                agent = agent
            };
        }
    }
}
