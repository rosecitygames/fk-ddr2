using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using RCG.Maps;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class EngageMapCell : AbstractCommand
    {
        IAgent agent = null;
        string completedTransition;

        protected override void OnStart()
        {
            agent.TargetAdvertisement = null;
            HandleMapCellMapElements();
        }

        protected override void OnStop()
        {
            
        }

        void HandleMapCellMapElements()
        {
            string mapElementDisplayNames = "Map Elements: ";

            int highestRank = 0;
            IMapElement highestRankedMapElement = null;

            List<IMapElement> mapElements = agent.Map.GetMapElementsAtCell(agent.Location);
            foreach (IMapElement mapElement in mapElements)
            {
                mapElementDisplayNames += mapElement.DisplayName +", ";
                int rank = GetMapElementRank(mapElement);
                if (rank > highestRank)
                {
                    highestRank = rank;
                    highestRankedMapElement = mapElement;
                }
            }

            Debug.Log(mapElementDisplayNames);
            if (highestRankedMapElement != null)
            {
                Debug.Log("Got " + highestRankedMapElement.DisplayName + "!");
                highestRankedMapElement.RemoveFromMap();
            }


            if (string.IsNullOrEmpty(completedTransition) == false)
            {
                agent.HandleTransition(completedTransition);
            }
        }

        int GetMapElementRank(IMapElement mapElement)
        {
            if (mapElement == agent)
            {
                return 0;
            }

            List<IAttribute> desires = agent.Desires;
            List<IAttribute> stats = mapElement.Stats;

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

        public static ICommand Create(IAgent agent, string completedTransition = "")
        {
            return new EngageMapCell
            {
                agent = agent,
                completedTransition = completedTransition
            };
        }
    }
}
