using IndieDevTools.Advertisements;
using IndieDevTools.Agents;
using IndieDevTools.Attributes;
using IndieDevTools.Commands;
using IndieDevTools.States;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Demo.BattleSimulator
{
    public class AdvertisementHandler : AbstractCommand
    {
        IAgent agent = null;
        string targetFoundTransition;

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
            if (rank > 0)
            {
                bool isRankGreater = GetIsAdRankGreaterThanTarget(advertisement, rank);
                if (isRankGreater)
                {
                    agent.TargetAdvertisement = RankedAdvertisement.Create(advertisement, rank);
                    agent.TargetLocation = advertisement.Location;

                    if (string.IsNullOrEmpty(targetFoundTransition) == false)
                    {
                        agent.HandleTransition(targetFoundTransition);
                    }
                }
            }       
        }

        int GetAdvertisementRank(IAdvertisement advertisement)
        {
            if (advertisement.GroupId == agent.GroupId) return 0;

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
                        float targetAdDistance = Vector2.Distance(agent.Location, agent.TargetAdvertisement.Location);
                        float adDistance = Vector2.Distance(agent.Location, advertisement.Location);
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

        public static ICommand Create(IAgent agent, string completedTransition = "")
        {
            return new AdvertisementHandler
            {
                agent = agent,
                targetFoundTransition = completedTransition
            };
        }
    }
}
