using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Advertisements
{
    public class AdvertisementBroadcaster : IAdvertisementBroadcaster
    {
        void IAdvertisementBroadcaster.Broadcast(IAdvertisement advertisement)
        {
            Broadcast(advertisement);
        }
        void IAdvertisementBroadcaster.Broadcast(IAdvertisement advertisement, IAdvertisementReceiver excludeReceiver)
        {
            Broadcast(advertisement, excludeReceiver);
        }
        protected void Broadcast(IAdvertisement advertisement, IAdvertisementReceiver excludeReceiver = null)
        {
            foreach(IAdvertisementReceiver receiver in receivers)
            {
                if (receiver != excludeReceiver)
                {
                    BroadcastToReceiverAtDistance(advertisement, receiver);
                }
            }
        }

        void BroadcastToReceiverAtDistance(IAdvertisement advertisement, IAdvertisementReceiver receiver)
        {
            float distance = Vector3.Distance(receiver.Location, advertisement.Location);
            if (distance <= advertisement.BroadcastDistance)
            {
                receiver.ReceiveAdvertisement(advertisement);
            }
        }

        void BroadcastToReceivers(IAdvertisement advertisement, List<IAdvertisementReceiver> receivers)
        {
            foreach (IAdvertisementReceiver receiver in receivers)
            {
                BroadcastToReceiver(advertisement, receiver);
            }
        }

        void BroadcastToReceiver(IAdvertisement advertisement, IAdvertisementReceiver receiver)
        {
            receiver.ReceiveAdvertisement(advertisement);
        }

        List<IAdvertisementReceiver> receivers = new List<IAdvertisementReceiver>();

        void IAdvertisementBroadcaster.AddReceiver(IAdvertisementReceiver receiver)
        {
            AddReceiver(receiver);
        }
        protected void AddReceiver(IAdvertisementReceiver receiver)
        {
            receivers.Add(receiver);
        }

        void IAdvertisementBroadcaster.RemoveReceiver(IAdvertisementReceiver receiver)
        {
            RemoveReceiver(receiver);
        }
        protected void RemoveReceiver(IAdvertisementReceiver receiver)
        {
            receivers.Remove(receiver);
        }

        void IAdvertisementBroadcaster.ClearReceivers()
        {
            ClearReceivers();
        }
        protected void ClearReceivers()
        {
            receivers.Clear();
        }

        public static IAdvertisementBroadcaster Create(List<IAdvertisementReceiver> receivers)
        {
            return new AdvertisementBroadcaster
            {
                receivers = receivers
            };
        }

        public static IAdvertisementBroadcaster Create()
        {
            return new AdvertisementBroadcaster();
        }
    }
}
