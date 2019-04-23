using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Advertisements;

namespace RCG.Items
{
    public class Item : MonoBehaviour , IItem
    {
        [SerializeField]
        ScriptableItemData data = null;
        IItemData itemData;
        IItemData IItem.ItemData
        {
            get
            {
                return ItemData;
            }
            set
            {
                ItemData = value;
            }
        }
        protected IItemData ItemData
        {
            get
            {
                InitItemData();
                return itemData;
            }
            set
            {
                itemData = value;
            }
        }

        void InitItemData()
        {
            if (itemData == null)
            {
                if (data == null)
                {
                    itemData = new NullItemData();
                }
                else
                {
                    itemData = (data as IItemData).Copy();
                }
            }
        }

        [SerializeField]
        ScriptableAdvertisementBroadcaster broadcaster = null;

        [SerializeField]
        float broadcastDistance = 0;
        float IAdvertisementBroadcastData.BroadcastDistance
        {
            get
            {
                return broadcastDistance;
            }
        }

        [SerializeField]
        float broadcastInterval;
        float IAdvertisementBroadcastData.BroadcastInterval
        {
            get
            {
                return broadcastDistance;
            }
        }

        IAdvertiser advertiser = null;
        protected IAdvertiser Advertiser
        {
            get
            {
                InitAdvertiser();
                return advertiser;
            }
            set
            {
                advertiser = value;
            }
        }

        void InitAdvertiser()
        {
            if (advertiser == null)
            {
                advertiser = Advertisements.Advertiser.Create(broadcaster);
            }
        }

        void IAdvertiser.SetBroadcaster(IAdvertisementBroadcaster broadcaster)
        {
            advertiser.SetBroadcaster(broadcaster);
        }

        void IAdvertiser.BroadcastAdvertisement(IAdvertisement advertisement)
        {
            BroadcastAdvertisement(advertisement);
        }

        void BroadcastAdvertisement()
        {
            IAdvertisement advertisement = Advertisement.Create(ItemData.Attributes, Location, broadcastDistance);
            Advertiser.BroadcastAdvertisement(advertisement);
        }

        void BroadcastAdvertisement(IAdvertisement advertisement)
        {
            advertiser.BroadcastAdvertisement(advertisement);
        }

        Vector2 ILocatable.Location { get { return Location; } }

        protected virtual Vector2 Location
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

        void Start()
        {
            InvokeRepeating("BroadcastAdvertisement", broadcastInterval, broadcastInterval);
        }

    }
}
