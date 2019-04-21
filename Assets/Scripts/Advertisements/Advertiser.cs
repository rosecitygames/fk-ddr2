using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Advertisements
{
    public class Advertiser : IAdvertiser
    {
        protected IAdvertisementBroadcaster Broadcaster { get; set; }

        void IAdvertiser.SetBroadcaster(IAdvertisementBroadcaster broadcaster)
        {
            Broadcaster = broadcaster;
        }

        void IAdvertiser.BroadcastAdvertisement(IAdvertisement advertisement)
        {
            BroadcastAdvertisement(advertisement);
        }

        protected void BroadcastAdvertisement(IAdvertisement advertisement)
        {
            if (Broadcaster == null) return;
            Broadcaster.Broadcast(advertisement);
        }

        public static IAdvertiser Create(IAdvertisementBroadcaster broadcaster)
        {
            return new Advertiser
            {
                Broadcaster = broadcaster
            };
        }

        public static IAdvertiser Create()
        {
            return new Advertiser();
        }
    }
}
