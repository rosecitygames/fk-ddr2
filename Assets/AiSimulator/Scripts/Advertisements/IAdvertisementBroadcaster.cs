using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Advertisements
{
    public interface IAdvertisementBroadcaster
    {
        void Broadcast(IAdvertisement advertisement);
        void Broadcast(IAdvertisement advertisement, IAdvertisementReceiver excludeReceiver);
        void BroadcastToReceivers(IAdvertisement advertisement, List<IAdvertisementReceiver> receivers);
        void BroadcastToReceiver(IAdvertisement advertisement, IAdvertisementReceiver receiver);

        void AddReceiver(IAdvertisementReceiver receiver);
        void RemoveReceiver(IAdvertisementReceiver receiver);
        void ClearReceivers();
    }
}
