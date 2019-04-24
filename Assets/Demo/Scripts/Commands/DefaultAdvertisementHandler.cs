using RCG.Advertisements;
using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using RCG.States;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class DefaultAdvertisementHandler : AbstractCommand
    {
        IAgent agent = null;

        protected override void OnStart()
        {
            AddEventHandler();
        }

        protected override void OnStop()
        {
            RemoveEventHandler();
        }

        protected override void OnDestroy()
        {
            RemoveEventHandler();
        }

        void AddEventHandler()
        {
            RemoveEventHandler();
            agent.OnAdvertisementReceived += HandleAdvertisement;
        }

        void RemoveEventHandler()
        {
            agent.OnAdvertisementReceived -= HandleAdvertisement;
        }

        void HandleAdvertisement(IAdvertisement advertisement)
        {
            int rank = GetAdvertisementRank(advertisement);
            bool isRankGreater = GetIsAdRankGreaterThanTarget(advertisement, rank);

            if (isRankGreater)
            {
                agent.TargetAdvertisement = RankedAdvertisement.Create(advertisement, rank);             
            }

            agent.HandleTransition("OnTargetAdFound");
        }

        int GetAdvertisementRank(IAdvertisement advertisement)
        {
            List<IAttribute> desires = agent.Desires;
            List<IAttribute> ads = advertisement.Attributes;

            int rank = 0;
            foreach (IAttribute attribute in ads)
            {
                foreach (IAttribute desire in desires)
                {
                    if (attribute.Id == desire.Id)
                    {
                        int attributeRank = attribute.Quantity * desire.Quantity;
                        if (attributeRank >= rank)
                        {
                            rank = attributeRank;
                        }
                    }
                }
            }
            return rank;
        }

        bool GetIsAdRankGreaterThanTarget(IAdvertisement advertisement, int rank)
        {
            bool isRankGreater = false;
            if (rank > 0)
            {
                bool hasTargetAdvertisement = agent.TargetAdvertisement != null;
                if (hasTargetAdvertisement)
                {
                    if (agent.TargetAdvertisement.Rank < rank)
                    {
                        isRankGreater = true;
                    }
                    else if (agent.TargetAdvertisement.Rank == rank)
                    {
                        float targetAdDistance = Vector3.Distance(agent.Location, agent.TargetAdvertisement.Location);
                        float adDistance = Vector3.Distance(agent.Location, advertisement.Location);
                        isRankGreater = targetAdDistance > adDistance;
                    }
                }
                else
                {
                    isRankGreater = true;
                }
            }

            return isRankGreater;
        }

        public static ICommand Create(IAgent agent)
        {
            return new DefaultAdvertisementHandler
            {
                agent = agent
            };
        }
    }
}
