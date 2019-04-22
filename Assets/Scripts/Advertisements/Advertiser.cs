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
            Broadcaster = broadcaster ?? NullAdvertisementBroadcaster.Create();
        }

        void IAdvertiser.BroadcastAdvertisement(IAdvertisement advertisement)
        {
            BroadcastAdvertisement(advertisement);
        }

        protected void BroadcastAdvertisement(IAdvertisement advertisement)
        {
            Broadcaster.Broadcast(advertisement);
        }

        public static IAdvertiser Create(IAdvertisementBroadcaster broadcaster)
        {
            return new Advertiser
            {
                Broadcaster = broadcaster ?? NullAdvertisementBroadcaster.Create()
            };
        }

        public static IAdvertiser Create()
        {
            return new Advertiser
            {
                Broadcaster = NullAdvertisementBroadcaster.Create()
            };
        }
    }
}
