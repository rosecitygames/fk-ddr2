using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Advertisements;
using RCG.Attributes;
using RCG.Maps;

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

        string IDescribable.DisplayName { get { return ItemData.DisplayName; } }
        string IDescribable.Description { get { return ItemData.Description; } }

        List<IAttribute> IStatsCollection.Stats { get { return ItemData.Stats; } }
        IAttribute IStatsCollection.GetStat(string id) { return ItemData.GetStat(id); }

        
        protected int GroupId { get; set; }
        int IGroupMember.GroupId
        {
            get
            {
                return GroupId;
            }
            set
            {
                GroupId = value;
            }
        }

        [SerializeField]
        AbstractMap map;
        IMap imap;
        protected IMap Map
        {
            get
            {
                if (imap == null)
                {
                    imap = map ?? NullMap.Create();
                }
                return imap;
            }
            set
            {
                imap = value;
            }
        }
        IMap IMapElement.Map
        {
            get
            {
                return Map;
            }
            set
            {
                Map = value;
            }
        }

        void IMapElement.AddToMap(IMap map)
        {
            Map.AddElement(this);
        }

        void IMapElement.RemoveFromMap()
        {
            Map.RemoveElement(this);
        }

        float IMapElement.Distance(IMapElement otherMapElement)
        {
            return Vector3Int.Distance(otherMapElement.Location, Location);
        }

        Vector3Int ILocatable.Location { get { return Location; } }

        protected virtual Vector3Int Location
        {
            get
            {
                return Map.LocalToCell(transform.position);
            }
            set
            {
                transform.position = Map.CellToLocal(value);
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
        float broadcastInterval = 0.0f;
        float IAdvertisementBroadcastData.BroadcastInterval
        {
            get
            {
                return broadcastInterval;
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
            IAdvertisement advertisement = Advertisement.Create(ItemData.Stats, Location, broadcastDistance);
            Advertiser.BroadcastAdvertisement(advertisement);
        }

        void BroadcastAdvertisement(IAdvertisement advertisement)
        {
            advertiser.BroadcastAdvertisement(advertisement);
        }

        void Start()
        {
            InvokeRepeating("BroadcastAdvertisement", broadcastInterval, broadcastInterval);
        }

    }
}
