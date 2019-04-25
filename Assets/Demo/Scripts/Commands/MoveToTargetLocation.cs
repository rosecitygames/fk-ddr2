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

        Coroutine moveCoroutine;

        const float minSpeed = 0.0f;
        const float maxSpeed = 10.0f;
        const float defaultSpeed = 0.0f;

        const string speedAttributeId = "speed";
        float Speed
        {
            get
            {
                IAttribute attribute = agent.GetStat(speedAttributeId);
                if (attribute == null)
                {
                    return defaultSpeed;
                }
                return Mathf.Clamp(attribute.Quantity * 1.0f, minSpeed, maxSpeed);
            }
        }

        float SpeedPercentage
        {
            get
            {
                return Mathf.InverseLerp(minSpeed, maxSpeed, Speed);
            }
        }

        const float minMoveSpeed = 0.0f;
        const float maxMoveSpeed = 0.05f;

        float MoveSpeed
        {
            get
            {
                return Mathf.Lerp(minMoveSpeed, maxMoveSpeed, SpeedPercentage);
            }
        }

        protected override void OnStart()
        {
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
            moveCoroutine = monoBehaviour.StartCoroutine(Move());
        }

        void StopMove()
        {
            if (moveCoroutine != null)
            {
                monoBehaviour.StopCoroutine(moveCoroutine);
            }
        }

        IEnumerator Move()
        {
            Vector3 targetPosition = agent.Map.CellToLocal(agent.TargetLocation);
            float moveSpeed = MoveSpeed;
            bool isLocationReached = false;
            while (isLocationReached == false)
            {
                yield return new WaitForEndOfFrame();
                float targetDistance = Vector2.Distance(agent.Position, targetPosition);
                isLocationReached = (targetDistance < 0.001f);
                if (isLocationReached == false)
                {
                    agent.Position = Vector2.MoveTowards(agent.Position, targetPosition, moveSpeed);
                }
            }

            Complete();
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

