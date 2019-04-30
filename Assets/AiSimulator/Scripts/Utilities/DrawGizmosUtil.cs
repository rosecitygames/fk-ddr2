﻿using RCG.Advertisements;
using RCG.Agents;
using UnityEngine;

namespace RCG.Utils
{
    public static class DrawGizmosUtil
    {
        public static void DrawTargetLocationLine(IAgent agent, Color color)
        {
            Vector3 position = agent.Position;
            Vector3 targetPosition = agent.Map.CellToLocal(agent.TargetLocation);
            Gizmos.color = color;
            Gizmos.DrawLine(position, targetPosition);
        }

        public static void DrawBroadcastDistanceSphere(Vector3 position, float broadcastDistance, Color baseColor)
        {
            Color gizmoColor = baseColor;
            /*
            gizmoColor.a = baseColor.a * 0.2f;
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(position, broadcastDistance);
            */
            gizmoColor.a = baseColor.a * 0.1f;
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(position, broadcastDistance);
        }
    }
}