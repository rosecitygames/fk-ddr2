using IndieDevTools.Advertisements;
using IndieDevTools.Traits;
using IndieDevTools.Maps;
using IndieDevTools.States;
using IndieDevTools.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Items
{
    public abstract class AbstractItem : MonoBehaviour, IItem
    {
        [SerializeField]
        ScriptableItemData data = null;
        IItemData iData;
        IItemData IItem.Data { get => Data; set => Data = value; }
        protected IItemData Data
        {
            get
            {
                InitData();
                return iData;
            }
            set
            {
                iData = value;
            }
        }

        protected virtual void InitData()
        {
            if (iData == null)
            {
                if (data == null)
                {
                    iData = new NullItemData();
                }
                else
                {
                    iData = (data as IItemData).Copy();
                }
            }
        }

        string IDescribable.DisplayName { get => Data.DisplayName; set => Data.DisplayName = value; }
        string IDescribable.Description { get => Data.Description; set => Data.Description = value; }
        event Action<IDescribable> IUpdatable<IDescribable>.OnUpdated { add { Data.OnUpdated += value; } remove { Data.OnUpdated -= value; } }

        List<ITrait> IStatsCollection.Stats => Data.Stats;
        ITrait IStatsCollection.GetStat(string id) => Data.GetStat(id);

        // Group Member implementations
        [SerializeField]
        int groupId;
        protected int GroupId { get => groupId; set => groupId = value; }
        int IGroupMember.GroupId { get => GroupId; set => GroupId = value; }

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

        void IMapElement.AddToMap() => AddToMap(Map);
        void IMapElement.AddToMap(IMap map) => AddToMap(map);
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

        bool IMapElement.IsOnMap
        {
            get
            {
                if (Map == null) return false;
                return Map.GetIsElementOnMap(this);
            }
        }

        float IMapElement.Distance(IMapElement otherMapElement) => Distance(otherMapElement);
        protected virtual float Distance(IMapElement otherMapElement) => 0;

        float IMapElement.Distance(Vector2Int otherLocation) => Distance(otherLocation);
        protected virtual float Distance(Vector2Int otherMapElement) => 0;

        int IMapElement.InstanceId => gameObject.GetInstanceID();

        int IMapElement.SortingOrder => SortingOrder;
        protected virtual int SortingOrder => Mathf.RoundToInt(Position.y * Map.CellSize.y * -100.0f);

        Vector2Int ILocatable.Location => Location;
        protected virtual Vector2Int Location => Map.LocalToCell(Position);

        event Action<ILocatable> IUpdatable<ILocatable>.OnUpdated
        {
            add { OnLocationUpdated += value; }
            remove { OnLocationUpdated -= value; }
        }
        Action<ILocatable> OnLocationUpdated;

        Vector3 IPositionable.Position { get => Position; set => Position = value; }
        protected virtual Vector3 Position
        {
            get
            {
                return transform.localPosition;
            }
            set
            {
                Vector2Int currentLocation = Location;
                Vector2Int newLocation = Map.LocalToCell(value);

                transform.localPosition = value;

                if (currentLocation != newLocation)
                {
                    Map.AddElement(this);
                    OnLocationUpdated?.Invoke(this);
                }
            }
        }

        [SerializeField]
        ScriptableAdvertisementBroadcaster broadcaster = null;

        float IAdvertisementBroadcastData.BroadcastDistance => BroadcastDistance;
        protected float BroadcastDistance => Data.BroadcastDistance;

        float IAdvertisementBroadcastData.BroadcastInterval => BroadcastInterval;
        protected float BroadcastInterval => Data.BroadcastInterval;

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
            InitData();
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