using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using System.Collections;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class ChooseAdjacentLocation : AbstractCommand
    {
        IAgent agent;

        protected override void OnStart()
        {
            SetTargetLocation();
            Complete();
        }

        void SetTargetLocation()
        {
            agent.TargetLocation = GetRandomAdjacentLocation();
        }

        Vector3Int GetRandomAdjacentLocation()
        {
            Vector3Int location = agent.Location;
            location.x += Random.Range(-1, 2);
            location.y += Random.Range(-1, 2);
            return location;
        }

        public static ICommand Create(IAgent agent)
        {
            ChooseAdjacentLocation command = new ChooseAdjacentLocation
            {
                agent = agent
            };

            return command;
        }
    }
}