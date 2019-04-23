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
        IState state = null;

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
            List<IAttribute> desires = agent.Desires;
            List<IAttribute> ads = advertisement.Attributes;

            int adRank = 0;
            foreach(IAttribute ad in ads)
            {
                foreach(IAttribute desire in desires)
                {
                    if (ad.Id == desire.Id)
                    {
                        int rank = ad.Quantity * desire.Quantity;
                        if (rank >= adRank)
                        {
                            adRank = rank;                    
                        }
                    }
                }   
            }

            bool isNewTargetAd = false;
            if (adRank > 0)
            {
                bool hasTargetAdvertisement = agent.TargetAdvertisement != null;
                if (hasTargetAdvertisement)
                {
                    if (agent.TargetAdvertisement.Rank < adRank)
                    {
                        isNewTargetAd = true;
                    }
                    else if (agent.TargetAdvertisement.Rank == adRank)
                    {
                        float targetAdDistance = Vector2.Distance(agent.Location, agent.TargetAdvertisement.Location);
                        float adDistance = Vector2.Distance(agent.Location, advertisement.Location);
                        isNewTargetAd = targetAdDistance > adDistance;
                    }
                }
                else
                {
                    isNewTargetAd = true;
                }
            }

            if (isNewTargetAd)
            {
                agent.TargetAdvertisement = RankedAdvertisement.Create(advertisement, adRank);             
            }

            state.HandleTransition("OnTargetAdFound");

            /*

            What about agent rank attribute? or does it simply boost ad quantity? yes
            
            What about team attribute? Agent -> TeamItem -> MapItem

            */
        }

        public static ICommand Create(IState state, IAgent agent)
        {
            return new DefaultAdvertisementHandler
            {
                state = state,
                agent = agent
            };
        }
    }
}
