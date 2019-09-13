using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Advertisements
{
    public interface IAdvertiser
    {
        IAdvertisementBroadcaster GetBroadcaster();
        void SetBroadcaster(IAdvertisementBroadcaster broadcaster);
        void BroadcastAdvertisement(IAdvertisement advertisement);
        void BroadcastAdvertisement(IAdvertisement advertisement, IAdvertisementReceiver excludeReceiver);
    }
}

