using RCG.Agents;
using RCG.Commands;
using RCG.Maps;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class EngageMapCell : AbstractCommand
    {
        IAgent agent = null;

        protected override void OnStart()
        {
            string mapElementDisplayNames = "Map Elements:";
            // Get all cell elements and interact with highest ranked element
            List<IMapElement> mapElements = agent.Map.GetMapElementsAtCell(agent.Location);
            foreach(IMapElement mapElement in mapElements)
            {
                mapElementDisplayNames += " "+mapElement.DisplayName;
            }
            Debug.Log(mapElementDisplayNames);
        }

        protected override void OnStop()
        {
            
        }

        public static ICommand Create(IAgent agent)
        {
            return new EngageMapCell
            {
                agent = agent
            };
        }
    }
}
