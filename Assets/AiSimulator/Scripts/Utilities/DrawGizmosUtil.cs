using IndieDevTools.Advertisements;
using IndieDevTools.Agents;
using UnityEngine;

namespace IndieDevTools.Utils
{
    public static class DrawGizmosUtil
    {
        public static void DrawTargetLocationLine(IAgent agent, Color color)
        {
            Vector3 position = agent.Position;
            Vector3 targetPosition = agent.Map.CellToLocal(agent.TargetAdvertisement.Location);
            Gizmos.color = color;
            Gizmos.DrawLine(position, targetPosition);
        }

        public static void DrawBroadcastDistanceSphere(Vector3 position, float broadcastDistance, Color baseColor)
        {
            Color gizmoColor = baseColor;

            gizmoColor.a = baseColor.a * 0.05f;
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(position, broadcastDistance);
        }

        public static void DrawBroadcastDistanceWireSphere(Vector3 position, float broadcastDistance, Color baseColor)
        {
            Color gizmoColor = baseColor;
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(position, broadcastDistance);
        }
    }
}
