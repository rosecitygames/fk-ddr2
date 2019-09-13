﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Advertisements
{
    public class NullAdvertisementBroadcaster : IAdvertisementBroadcaster
    {
        void IAdvertisementBroadcaster.Broadcast(IAdvertisement advertisement) { }
        void IAdvertisementBroadcaster.Broadcast(IAdvertisement advertisement, IAdvertisementReceiver excludeReceiver) { }
        void IAdvertisementBroadcaster.AddReceiver(IAdvertisementReceiver receiver) { }
        void IAdvertisementBroadcaster.RemoveReceiver(IAdvertisementReceiver receiver) { }
        void IAdvertisementBroadcaster.ClearReceivers() { }

        public static IAdvertisementBroadcaster Create()
        {
            return new NullAdvertisementBroadcaster();
        }
    }
}
