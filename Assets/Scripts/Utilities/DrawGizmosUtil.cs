using RCG.Advertisements;
using RCG.Agents;
using UnityEngine;

namespace RCG.Utils
{
    public static class DrawGizmosUtil
    {
        public static void DrawTargetAdvertisementLine(Vector3 position, Vector3 targetAdvertisementPosition, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(position, targetAdvertisementPosition);
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
