using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Advertisements
{
    [CreateAssetMenu(fileName = "AdvertisementBroadcaster", menuName = "RCG/Advertisement Broadcaster")]
    public class ScriptableAdvertisementBroadcaster : ScriptableObject, IAdvertisementBroadcaster
    {
        [System.NonSerialized]
        IAdvertisementBroadcaster broadcaster = AdvertisementBroadcaster.Create();

        void IAdvertisementBroadcaster.Broadcast(IAdvertisement advertisement)
        {
            broadcaster.Broadcast(advertisement);
        }

        void IAdvertisementBroadcaster.Broadcast(IAdvertisement advertisement, IAdvertisementReceiver excludeReceiver)
        {
            broadcaster.Broadcast(advertisement, excludeReceiver);
        }

        void IAdvertisementBroadcaster.BroadcastToReceivers(IAdvertisement advertisement, List<IAdvertisementReceiver> receivers)
        {
            broadcaster.BroadcastToReceivers(advertisement, receivers);
        }

        void IAdvertisementBroadcaster.BroadcastToReceiver(IAdvertisement advertisement, IAdvertisementReceiver receiver)
        {
            broadcaster.BroadcastToReceiver(advertisement, receiver);
        }

        void IAdvertisementBroadcaster.AddReceiver(IAdvertisementReceiver receiver)
        {
            broadcaster.AddReceiver(receiver);
        }

        void IAdvertisementBroadcaster.RemoveReceiver(IAdvertisementReceiver receiver)
        {
            broadcaster.RemoveReceiver(receiver);
        }

        void IAdvertisementBroadcaster.ClearReceivers()
        {
            broadcaster.ClearReceivers();
        }
    }
}
