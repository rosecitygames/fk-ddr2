﻿using RCG.Advertisements;
using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using System.Collections.Generic;
using System.Linq;
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
            List<IAttribute> desires = (agent as IDesiresCollection).Desires;
            List<IAttribute> ads = advertisement.Attributes;
            IAttribute mostDesireableAd = ads.Where(ad => desires.All(desire => ad.Id == desire.Id)).OrderByDescending(ad => ad.Quantity).Last();

            Debug.Log("mostDesireableAd = " + mostDesireableAd.DisplayName);

            /*
              
            What about rank attribute? or does it simply boost ad quantity? yes
            
            What about team attribute? check it

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