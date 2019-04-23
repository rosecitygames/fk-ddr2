using RCG.Advertisements;
using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using System.Collections;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class MoveAgentToTargetAdLocation : AbstractCommand
    {
        AbstractAgent agent;

        Vector2 AgentLocation
        {
            get
            {
                return (agent as IAgent).Location;
            }
        }

        IAdvertisement TargetAdvertisement
        {
            get
            {
                return (agent as IAgent).TargetAdvertisement;
            }
        }

        const float minSpeed = 0.0f;
        const float maxSpeed = 10.0f;
        const float defaultSpeed = 0.0f;

        const string speedAttributeId = "speed";
        float Speed
        {
            get
            {
                IAttribute attribute = (agent as IStatsCollection).GetStat(speedAttributeId);
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
        const float maxMoveSpeed = 0.1f;

        float MoveSpeed
        {
            get
            {
                return Mathf.Lerp(minMoveSpeed, maxMoveSpeed, SpeedPercentage);
            }
        }

        const float minMoveIntervalSeconds = 0.0f;
        const float maxMoveIntervalSeconds = 2.0f;

        float MoveIntervalSeconds
        {
            get
            {
                float lerpPercentage = 1.0f - SpeedPercentage;
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
            float moveSpeed = MoveSpeed;

            bool isLocationReached = false;
            while (isLocationReached == false)
            {
                //yield return new WaitForSeconds(MoveIntervalSeconds);
                yield return new WaitForEndOfFrame();

                if (TargetAdvertisement == null)
                {
                    isLocationReached = true;
                }
                else
                {
                    float targetDistance = Vector2.Distance(AgentLocation, TargetAdvertisement.Location);
                    if (targetDistance < 0.01f)
                    {
                        isLocationReached = true;
                    }
                    else
                    {
                       agent.transform.position =  Vector2.MoveTowards(AgentLocation, TargetAdvertisement.Location, moveSpeed);
                    }
                }
            }

            Complete();
        }

        public static ICommand Create(AbstractAgent agent)
        {
            MoveAgentToTargetAdLocation command = new MoveAgentToTargetAdLocation
            {
                agent = agent
            };

            return command;
        }
    }
}

