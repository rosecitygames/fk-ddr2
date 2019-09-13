﻿using IndieDevTools.Advertisements;
using IndieDevTools.Attributes;
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
        IItemData itemData;
        IItemData IItem.ItemData { get => ItemData; set => ItemData = value; }
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

        string IDescribable.DisplayName => ItemData.DisplayName;
        string IDescribable.Description => ItemData.Description;

        List<IAttribute> IStatsCollection.Stats => ItemData.Stats;
        IAttribute IStatsCollection.GetStat(string id) => ItemData.GetStat(id);

        int IGroupMember.GroupId => GroupId;
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

        event Action<Vector2Int> ILocatable.OnUpdated
        {
            add { OnLocationUpdated += value; }
            remove { OnLocationUpdated -= value; }
        }
        Action<Vector2Int> OnLocationUpdated;

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
                    OnLocationUpdated?.Invoke(newLocation);
                }
            }
        }

        [SerializeField]
        ScriptableAdvertisementBroadcaster broadcaster = null;

        float IAdvertisementBroadcastData.BroadcastDistance => BroadcastDistance;
        protected float BroadcastDistance => ItemData.BroadcastDistance;

        float IAdvertisementBroadcastData.BroadcastInterval => BroadcastInterval;
        protected float BroadcastInterval => ItemData.BroadcastInterval;

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