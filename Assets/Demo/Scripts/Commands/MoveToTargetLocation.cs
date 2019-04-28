using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using System.Collections;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class MoveToTargetLocation : AbstractCommand
    {
        IAgent agent;
        MonoBehaviour monoBehaviour;

        Coroutine coroutine;

        SpriteRenderer spriteRenderer;

        protected override void OnStart()
        {
            spriteRenderer = monoBehaviour.GetComponentInChildren<SpriteRenderer>();
            StartMove();
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
            float moveSpeed = AttributesUtil.GetMoveSpeed(agent);
            bool isLocationReached = false;
            while (isLocationReached == false)
            {
                yield return new WaitForEndOfFrame();
                float targetDistance = Vector2.Distance(agent.Position, targetPosition);
                isLocationReached = (targetDistance < (agent.Map.CellSize.x * 0.5f));
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
            MoveToTargetLocation command = new MoveToTargetLocation
            {
                agent = agent,
                monoBehaviour = agent
            };

            return command;
        }
    }
}

