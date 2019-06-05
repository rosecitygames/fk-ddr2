using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Maps;

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
            // TODO : Need to cache cells somewhere for stationary broadcasters. Maybe ad method to advertise at list of cells.

            IMap adMap = advertisement.Map;
            Vector3Int adLocation = advertisement.Location;
            int broadcastDistance = Mathf.RoundToInt(advertisement.BroadcastDistance);

            int size = broadcastDistance * 2;
    
            List<Vector3Int> cells = new List<Vector3Int>();
            int cellX, cellY;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    cellX = (adLocation.x + x) - broadcastDistance;
                    cellY = (adLocation.y + y) - broadcastDistance;

                    if ((cellX >= -adMap.Size.x && cellX < adMap.Size.x) && (cellY >= -adMap.Size.y && cellY < adMap.Size.y))
                    {
                        Vector3Int cell = new Vector3Int(cellX, cellY, 0);
                        cells.Add(cell);
                    }  
                }
            }

            List<IMapElement> mapElements = adMap.GetMapElementsAtCells(cells);
            foreach(IMapElement mapElement in mapElements)
            {
                if (receiversByMapElement.ContainsKey(mapElement))
                {
                    IAdvertisementReceiver receiver = receiversByMapElement[mapElement];
                    receiver.ReceiveAdvertisement(advertisement);
                }
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

        Dictionary<IMapElement, IAdvertisementReceiver> receiversByMapElement = new Dictionary<IMapElement, IAdvertisementReceiver>();

        void IAdvertisementBroadcaster.AddReceiver(IAdvertisementReceiver receiver)
        {
            AddReceiver(receiver);
        }
        protected void AddReceiver(IAdvertisementReceiver receiver)
        {
            receiversByMapElement.Add(receiver, receiver);
        }

        void IAdvertisementBroadcaster.RemoveReceiver(IAdvertisementReceiver receiver)
        {
            RemoveReceiver(receiver);
        }
        protected void RemoveReceiver(IAdvertisementReceiver receiver)
        {
            receiversByMapElement.Remove(receiver);
        }

        void IAdvertisementBroadcaster.ClearReceivers()
        {
            ClearReceivers();
        }
        protected void ClearReceivers()
        {
            receiversByMapElement.Clear();
        }

        public static IAdvertisementBroadcaster Create(List<IAdvertisementReceiver> receivers)
        {
            Dictionary<IMapElement, IAdvertisementReceiver> receiversByMapElement = new Dictionary<IMapElement, IAdvertisementReceiver>();
            foreach (IAdvertisementReceiver receiver in receivers)
            {
                receiversByMapElement.Add(receiver, receiver);
            }

            return new AdvertisementBroadcaster
            {
                receiversByMapElement = receiversByMapElement
            };
        }

        public static IAdvertisementBroadcaster Create()
        {
            return new AdvertisementBroadcaster();
        }
    }
}
