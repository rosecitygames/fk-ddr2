using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using System.Collections;
using UnityEngine;

namespace RCG.Demo.BattleSimulator
{
    public class OffsetPositionFromTargetMapElement : AbstractCommand
    {
        IAgent agent;
        MonoBehaviour monoBehaviour;

        Coroutine coroutine;

        SpriteRenderer spriteRenderer;

        protected override void OnStart()
        {
            spriteRenderer = monoBehaviour.GetComponentInChildren<SpriteRenderer>();

            if (agent.TargetMapElement != null && agent.Location == agent.TargetMapElement.Location)
            {
                StartMove();
            }
            else
            {
                Complete();
            }
        }

        protected override void OnStop()
        {
            StopMove();
        }

        protected override void OnDestroy()
        {
            StopMove();
        }

        void StartMove()
        {
            StopMove();
            coroutine = monoBehaviour.StartCoroutine(Move());
        }

        void StopMove()
        {
            if (coroutine != null)
            {
                monoBehaviour.StopCoroutine(coroutine);
            }
        }

        IEnumerator Move()
        {
            Vector3 targetPosition = agent.Map.CellToLocal(agent.TargetLocation);

            float offsetX = agent.Map.CellSize.x * 0.25f;
            if (agent.InstanceId < agent.TargetMapElement.InstanceId)
            {
                offsetX *= -1.0f;
            }

            targetPosition.x += offsetX;

            float moveSpeed = AttributesUtil.GetMoveSpeed(agent);

            YieldInstruction yieldInstruction = new WaitForEndOfFrame();

            bool isLocationReached = false;
            while (isLocationReached == false)
            {
                yield return yieldInstruction;
                float targetDistance = Vector2.Distance(agent.Position, targetPosition);
                isLocationReached = targetDistance < 0.001f;
                if (isLocationReached == false)
                {
                    agent.Position = Vector2.MoveTowards(agent.Position, targetPosition, moveSpeed);
                }
                UpdateSortingOrder();
            }

            Complete();
        }

        void UpdateSortingOrder()
        {
            if (spriteRenderer == null) return;
            spriteRenderer.sortingOrder = agent.SortingOrder;
        }

        public static ICommand Create(AbstractAgent agent)
        {
            OffsetPositionFromTargetMapElement command = new OffsetPositionFromTargetMapElement
            {
                agent = agent,
                monoBehaviour = agent
            };

            return command;
        }
    }
}

