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

        string IDescribable.DisplayName { get => ItemData.DisplayName; }
        string IDescribable.Description { get => ItemData.Description; }

        List<IAttribute> IStatsCollection.Stats { get => ItemData.Stats; }
        IAttribute IStatsCollection.GetStat(string id) { return ItemData.GetStat(id); }

        int IGroupMember.GroupId { get => GroupId; set => GroupId = value; }
        protected virtual int GroupId { get; set; }     

        // Map implementations
        IMap map;
        IMap IMapElement.Map { get => Map; set => Map = value; }
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
            return Distance(otherMapElement);
        }
        protected virtual float Distance(IMapElement otherMapElement)
        {
            return Vector3Int.Distance(otherMapElement.Location, Location);
        }

        int IMapElement.SortingOrder {  get => SortingOrder; }
        protected virtual int SortingOrder { get => Mathf.RoundToInt(Position.y * Map.CellSize.y * -100.0f); }

        Vector3Int ILocatable.Location { get => Location; }
        protected virtual Vector3Int Location { get => Map.LocalToCell(Position); }

        Vector3 IPositionable.Position { get => Position; set => Position = value; }
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

        float IAdvertisementBroadcastData.BroadcastDistance { get => BroadcastDistance; }
        protected float BroadcastDistance { get => ItemData.BroadcastDistance; }

        float IAdvertisementBroadcastData.BroadcastInterval { get => BroadcastInterval; }
        protected float BroadcastInterval { get => ItemData.BroadcastInterval; }

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
            advertiser.BroadcastAdvertisement(advertisement);
        }

        void IAdvertiser.BroadcastAdvertisement(IAdvertisement advertisement, IAdvertisementReceiver excludeReceiver)
        {
            advertiser.BroadcastAdvertisement(advertisement, excludeReceiver);
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