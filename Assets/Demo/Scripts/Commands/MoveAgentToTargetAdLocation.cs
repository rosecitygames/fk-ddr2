using RCG.Advertisements;
using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using System.Collections;
using UnityEngine;
using RCG.Maps;

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


                Grid grid = agent.transform.parent.GetComponent<Grid>();
                Vector3Int cellPosition = grid.LocalToCell(agent.transform.localPosition);
                Vector3 localPosition = grid.CellToLocal(cellPosition);
                Debug.Log("CellPosition = " + cellPosition);
                
                if (TargetAdvertisement == null)
                {
                    isLocationReached = true;
                }
                else
                {
                    float targetDistance = Vector3Int.Distance(AgentLocation, TargetAdvertisement.Location);
                    if (targetDistance == 0)
                    {
                        isLocationReached = true;
                    }
                    else
                    {
                        Vector3 location = Vector3.MoveTowards(AgentLocation, TargetAdvertisement.Location, moveSpeed);
                        Vector3Int mapLocation = new Vector3Int(Mathf.RoundToInt(location.x), Mathf.RoundToInt(location.y), 0);
                        //AgentLocation = mapLocation;
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

