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

            IAttribute highestRankedAd = null;
            int highestAdRank = 1;
            foreach(IAttribute ad in ads)
            {
                foreach(IAttribute desire in desires)
                {
                    if (ad.Id == desire.Id)
                    {
                        int rank = ad.Quantity * desire.Quantity;
                        if (rank >= highestAdRank)
                        {
                            highestAdRank = rank;
                            highestRankedAd = ad;                     
                        }
                    }
                }
            }

            if (highestRankedAd == null) return;
            Debug.Log(agent.DisplayName + " found " + highestRankedAd.DisplayName+"!");

            /*
              
            What about rank attribute? or does it simply boost ad quantity? yes
            
            What about team attribute? Agent -> TeamItem -> MapItem

            If at location, call transition "OnMoveToAdvertisementCompleted_X"

            Else, call transition "OnMoveToAdvertisement_X"

            OR

            set agent.currentDesiredAdvertisement

            the, call transition MoveToCurrentDesiredAdvertisement

            OR



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
