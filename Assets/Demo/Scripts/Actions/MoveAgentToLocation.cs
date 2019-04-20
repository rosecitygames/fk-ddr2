using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Actions;
using RCG.Agents;

namespace RCG.Demo.Simulator
{
    public class MoveAgentToLocation : AbstractAction
    {
        AbstractAgent agent = null;

        Vector2 targetLocation = Vector2.zero;

        const float minSpeed = 0.0f;
        const float maxSpeed = 10.0f;
        const float defaultSpeed = 0.0f;

        const string speedAttributeId = "speed";
        float Speed
        {
            get
            {
                IAttribute attribute = (agent as IStatsCollection).GetStat(speedAttributeId);
                if(attribute == null)
                {
                    return defaultSpeed;
                }
                return Mathf.Clamp(attribute.Quantity * 1.0f, minSpeed, maxSpeed);
            }
        }

        const float minMoveIntervalSeconds = 0.0f;
        const float maxMoveIntervalSeconds = 2.0f;

        float MoveIntervalSeconds
        {
            get
            {
                float lerpPercentage = Mathf.InverseLerp(minSpeed, maxSpeed, Speed);
                lerpPercentage = 1.0f - lerpPercentage;
                float moveIntervalSeconds = Mathf.Lerp(minMoveIntervalSeconds, maxMoveIntervalSeconds, lerpPercentage);
                return moveIntervalSeconds;
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
            agent.StartCoroutine(Move());
        }

        void StopMove()
        {
            agent.StopCoroutine(Move());
        }

        IEnumerator Move()
        {
            bool isLoactionReached = false;
            while(isLoactionReached == false)
            {
                yield return new WaitForSeconds(MoveIntervalSeconds);
                // TODO Move towards location
            }

            Complete();
        }

        public static IAction Create(AbstractAgent agent, Vector2 targetLocation)
        {
            return new MoveAgentToLocation
            {
                agent = agent,
                targetLocation = targetLocation
            };
        }
    }
}

