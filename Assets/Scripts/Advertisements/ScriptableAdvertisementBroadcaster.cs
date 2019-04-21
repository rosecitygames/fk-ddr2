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
