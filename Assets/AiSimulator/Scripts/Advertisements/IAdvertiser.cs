using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Advertisements
{
    public interface IAdvertiser
    {
        IAdvertisementBroadcaster GetBroadcaster();
        void SetBroadcaster(IAdvertisementBroadcaster broadcaster);
        void BroadcastAdvertisement(IAdvertisement advertisement);
        void BroadcastAdvertisement(IAdvertisement advertisement, IAdvertisementReceiver excludeReceiver);
    }
}

