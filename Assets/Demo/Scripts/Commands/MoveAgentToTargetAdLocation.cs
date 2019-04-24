using RCG.Advertisements;
using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using RCG.Maps;
using System.Collections;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class MoveAgentToTargetAdLocation : AbstractCommand
    {
        AbstractAgent agent;

        Vector3Int AgentLocation
        {
            get
            {
                return (agent as IAgent).Location;
            }
        }

        Vector3 AgentLocalPosition
        {
            get
            {
                return agent.transform.localPosition;
            }
            set
            {
                agent.transform.localPosition = value;
                Vector3Int mapLocation = (agent as IAgent).Map.LocalToCell(agent.transform.position);
                (agent as IAgent).Location = mapLocation;
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
            agent.StartCoroutine(Move());
        }

        void StopMove()
        {
            agent.StopCoroutine(Move());
        }

        IEnumerator Move()
        {
            float moveSpeed = MoveSpeed;
            Vector3Int currentAgentLocation = AgentLocation;

            bool isLocationReached = false;
            while (isLocationReached == false)
            {
                yield return new WaitForEndOfFrame();

                
                if (TargetAdvertisement == null)
                {
                    isLocationReached = true;
                }
                else
                {
                    Vector3 targetAdvertisementPosition = GetTargetAdvertisementPosition();
                    float targetDistance = Vector2.Distance(AgentLocalPosition, targetAdvertisementPosition);
                    if (targetDistance < 0.001f)
                    {
                        isLocationReached = true;
                    }
                    else
                    {
                        AgentLocalPosition =  Vector2.MoveTowards(AgentLocalPosition, targetAdvertisementPosition, moveSpeed);                      
                    }              
                }
            }

            Complete();
        }

        Vector3 GetTargetAdvertisementPosition()
        {
            IMap map = (agent as IMapElement).Map;
            return map.CellToLocal(TargetAdvertisement.Location);
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

