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

        // Map implementations
        IMap map;

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

        protected IMap Map
        {
            get
            {
                InitMap();
                return map;
            }
            set
            {
                map = value;
            }
        }

        void InitMap()
        {
            if (map == null)
            {
                map = GetComponentInParent<IMap>() ?? NullMap.Create();
                map.AddElement(this);
            }
        }

        void IMapElement.AddToMap(IMap map)
        {
            Map.RemoveElement(this);
            this.map = map;
            Map.AddElement(this);
        }

        void IMapElement.RemoveFromMap()
        {
            Map.RemoveElement(this);
            StopBroadcastingAdvertisement();
            Destroy(gameObject); // TODO : Make this an abstract class and let concrete implementation handle. Will want animation or sound.
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
        }

        Vector3 IPositionable.Position { get { return Position; } set { Position = value; } }
        protected virtual Vector3 Position
        {
            get
            {
                return transform.localPosition;
            }
            set
            {
                Vector3Int newLocation = Map.LocalToCell(value);

                transform.localPosition = value;

                if (Location != newLocation)
                {
                    Map.AddElement(this);
                }
            }
        }


        [SerializeField]
        ScriptableAdvertisementBroadcaster broadcaster = null;

        float IAdvertisementBroadcastData.BroadcastDistance { get { return BroadcastDistance; } }
        float BroadcastDistance { get { return ItemData.BroadcastDistance; } }

        float IAdvertisementBroadcastData.BroadcastInterval { get { return BroadcastInterval; } }
        float BroadcastInterval { get { return ItemData.BroadcastInterval; } }

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
            IAdvertisement advertisement = Advertisement.Create(ItemData.Stats, Location, BroadcastDistance);
            Advertiser.BroadcastAdvertisement(advertisement);
        }

        void BroadcastAdvertisement(IAdvertisement advertisement)
        {
            advertiser.BroadcastAdvertisement(advertisement);
        }

        void Start()
        {
            Init();
        }

        void Init()
        {
            InitItemData();
            InitAdvertiser();
            InitMap();
            StartBroadcastingAdvertisement();
        }

        void StartBroadcastingAdvertisement()
        {
            InvokeRepeating("BroadcastAdvertisement", BroadcastInterval, BroadcastInterval);
        }

        void StopBroadcastingAdvertisement()
        {
            CancelInvoke("BroadcastAdvertisement");
        }

        private void OnDrawGizmos()
        {
            float broadcastDistance = (data as IItemData).BroadcastDistance * 0.2f;

            Color gizmoColor = Color.yellow;

            gizmoColor.a = 0.2f;
            Gizmos.color = gizmoColor;          
            Gizmos.DrawWireSphere(transform.position, broadcastDistance);

            gizmoColor.a = 0.1f;
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, broadcastDistance);
        }

    }
}
