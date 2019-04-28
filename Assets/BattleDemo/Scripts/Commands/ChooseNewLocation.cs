using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using System.Collections;
using UnityEngine;

namespace RCG.Demo.BattleSimulator
{
    public class ChooseNewLocation : AbstractCommand
    {
        IAgent agent;

        protected override void OnStart()
        {
            SetTargetLocation();
            Complete();
        }

        void SetTargetLocation()
        {
            agent.TargetLocation = GetNewLocation();
        }

        Vector3Int GetNewLocation()
        {
            int moveRadius = AttributesUtil.GetMoveRadius(agent);
            Vector3Int location = agent.Location;

            Bounds mapBounds = new Bounds(Vector3.zero, agent.Map.Size);
            bool isInBounds = false;

            while(isInBounds == false)
            {
                Vector2Int offset = Vector2Int.RoundToInt(Random.insideUnitCircle * moveRadius);
                location = agent.Location;
                location.x += offset.x;
                location.y += offset.y;

                isInBounds = mapBounds.Contains(location);
            }

            return location;
        }

        public static ICommand Create(IAgent agent)
        {
            ChooseNewLocation command = new ChooseNewLocation
            {
                agent = agent
            };

            return command;
        }
    }
}