using RCG.Advertisements;
using RCG.Attributes;
using RCG.Maps;
using RCG.States;
using RCG.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Items
{
    public abstract class AbstractItem : MonoBehaviour, IItem
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
                if (itemData == null)
                {
                    InitItemData();
                }
                return itemData;
            }
            set
            {
                itemData = value;
            }
        }

        protected virtual void InitItemData()
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
                if (map == null)
                {
                    InitMap();
                }
                return map;
            }
            set
            {
                map = value;
            }
        }

        protected virtual void InitMap()
        {
            map = GetComponentInParent<IMap>() ?? NullMap.Create();
            map.AddElement(this);
        }

        void IMapElement.AddToMap(IMap map)
        {
            AddToMap(map);
        }
        protected virtual void AddToMap(IMap map)
        {
            if (map != null)
            {
                Map.RemoveElement(this);
            }
            this.map = map;
            Map.AddElement(this);
        }

        void IMapElement.RemoveFromMap()
        {
            RemoveFromMap();
        }
        protected virtual void RemoveFromMap()
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
                return Map.LocalToCell(Position);
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
                Vector3Int currentLocation = Location;
                Vector3Int newLocation = Map.LocalToCell(value);

                transform.localPosition = value;

                if (currentLocation != newLocation)
                {
                    Map.AddElement(this);
                }
            }
        }

        [SerializeField]
        ScriptableAdvertisementBroadcaster broadcaster = null;

        float IAdvertisementBroadcastData.BroadcastDistance { get { return BroadcastDistance; } }
        protected float BroadcastDistance { get { return ItemData.BroadcastDistance; } }

        float IAdvertisementBroadcastData.BroadcastInterval { get { return BroadcastInterval; } }
        protected float BroadcastInterval { get { return ItemData.BroadcastInterval; } }

        IAdvertiser advertiser = null;
        protected IAdvertiser Advertiser
        {
            get
            {
                if (advertiser == null)
                {
                    InitAdvertiser();
                }
                return advertiser;
            }
            set
            {
                advertiser = value;
            }
        }

        protected virtual void InitAdvertiser()
        {
            advertiser = Advertisements.Advertiser.Create(broadcaster);
        }

        IAdvertisementBroadcaster IAdvertiser.GetBroadcaster()
        {
            return advertiser.GetBroadcaster();
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

        // State Machine implementations
        protected IStateMachine stateMachine = StateMachine.Create();

        protected virtual void InitStateMachine() { }

        void IStateTransitionHandler.HandleTransition(string transitionName)
        {
            stateMachine.HandleTransition(transitionName);
        }

        // Initialization
        void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            InitItemData();
            InitAdvertiser();
            InitMap();
            InitStateMachine();
        }

        // Cleanup
        void OnDestroy()
        {
            Cleanup();
        }

        protected virtual void Cleanup()
        {
            RemoveFromMap();


            if (stateMachine != null)
            {
                stateMachine.Destroy();
            }
        }

        void OnDrawGizmos()
        {
            if (data != null && Map != null)
            {
                float broadcastDistance = (data as IItemData).BroadcastDistance * 0.2f;
                DrawGizmosUtil.DrawBroadcastDistanceSphere(Position, broadcastDistance, Color.yellow);
            }
        }

    }
}