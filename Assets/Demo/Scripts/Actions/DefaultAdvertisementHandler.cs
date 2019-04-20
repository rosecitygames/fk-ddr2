using System.Linq;
using System.Collections.Generic;
using RCG.Actions;
using RCG.Agents;
using RCG.Advertisements;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class DefaultAdvertisementHandler : AbstractAction
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
            List<IAttribute> desires = (agent as IDesiresCollection).Desires;
            List<IAttribute> ads = advertisement.Attributes;
            IAttribute mostDesireableAd = ads.Where(ad => desires.All(desire => ad.Id == desire.Id)).OrderByDescending(ad => ad.Quantity).Last();

            Debug.Log("mostDesireableAd = " + mostDesireableAd.DisplayName);

            /*
              
            What about rank attribute? or does it simply boost ad quantity?
            
            What about team attribute?

            If at location, call transition "OnMoveToAdvertisementCompleted_X"

            Else, call transition "OnMoveToAdvertisement_X"

            OR

            set agent.currentDesiredAdvertisement

            the, call transition MoveToCurrentDesiredAdvertisement

            OR



            */
        }

        public static IAction Create(IAgent agent)
        {
            return new DefaultAdvertisementHandler
            {
                agent = agent
            };
        }
    }
}
