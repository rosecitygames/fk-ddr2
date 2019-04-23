using RCG.Advertisements;
using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
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

            if (adRank <= 0) return;

            if (agent.DesiredAdvertisement == null || agent.DesiredAdvertisement.Rank < adRank)
            {
                agent.DesiredAdvertisement = RankedAdvertisement.Create(advertisement, adRank);
            }

            Debug.Log(agent.DisplayName + " seeking " + agent.DesiredAdvertisement + "!");

            // SWITCH STATE to MoveToDesiredAdvertisement
           
            /*

            What about agent rank attribute? or does it simply boost ad quantity? yes
            
            What about team attribute? Agent -> TeamItem -> MapItem

            */
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
