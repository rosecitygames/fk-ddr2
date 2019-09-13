using IndieDevTools.Advertisements;
using IndieDevTools.Attributes;
using IndieDevTools.Maps;
using IndieDevTools.States;
using IndieDevTools.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Agents
{
    public abstract class AbstractAgent : MonoBehaviour, IAgent
    {
        // Agent Data implementations
        [SerializeField]
        ScriptableAgentData data = null;
        IAgentData agentData;
        IAgentData IAgent.AgentData { get => AgentData; set => AgentData = value; }
        protected IAgentData AgentData
        {
            get
            {
                if (agentData == null)
                {
                    InitAgentData();
                }             
                return agentData;
            }
            set
            {
                agentData = value;
            }
        }

        protected virtual void InitAgentData()
        {
            if (data == null)
            {
                agentData = new NullAgentData();
            }
            else
            {
                agentData = (data as IAgentData).Copy();
            }
        }

        string IDescribable.DisplayName => AgentData.DisplayName;
        string IDescribable.Description => AgentData.Description;

        List<IAttribute> IStatsCollection.Stats => AgentData.Stats;
        IAttribute IStatsCollection.GetStat(string id) => AgentData.GetStat(id);

        List<IAttribute> IDesiresCollection.Desires => AgentData.Desires;
        IAttribute IDesiresCollection.GetDesire(string id) => AgentData.GetDesire(id);

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

        bool IMapElement.IsOnMap => IsOnMap;
        protected bool IsOnMap
        {
            get
            {
                if (Map == null) return false;
                return Map.GetIsElementOnMap(this);
            }
        }

        float IMapElement.Distance(IMapElement otherMapElement) => Vector2Int.Distance(otherMapElement.Location, Location);
        float IMapElement.Distance(Vector2Int otherLocation) => Vector2Int.Distance(otherLocation, Location);

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

        // Broadcaster & Advertiser implementations
        [SerializeField]
        ScriptableAdvertisementBroadcaster broadcaster = null;

        float IAdvertisementBroadcastData.BroadcastDistance => BroadcastDistance;
        protected float BroadcastDistance => AgentData.BroadcastDistance;

        float IAdvertisementBroadcastData.BroadcastInterval => BroadcastInterval;
        protected float BroadcastInterval => AgentData.BroadcastInterval;

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
            InitBroadcaster();
            advertiser = Advertisements.Advertiser.Create(broadcaster);
        }

        void InitBroadcaster()
        {
            if (broadcaster != null)
            {
                (broadcaster as IAdvertisementBroadcaster).AddReceiver(this);
            }
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

        void IAdvertisementReceiver.ReceiveAdvertisement(IAdvertisement advertisement)
        {
            OnAdvertisementReceived?.Invoke(advertisement);
        }

        event Action<IAdvertisement> IAgent.OnAdvertisementReceived
        {
            add
            {
                OnAdvertisementReceived += value;
            }
            remove
            {
                OnAdvertisementReceived -= value;
            }
        }

        Action<IAdvertisement> OnAdvertisementReceived;

        IRankedAdvertisement IAgent.TargetAdvertisement { get; set; }

        IMapElement IAgent.TargetMapElement { get; set; }

        Vector2Int IAgent.TargetLocation { get; set; }

        // Group Member implementations
        [SerializeField]
        int groupId;
        protected int GroupId { get => groupId; set => groupId = value; }
        int IGroupMember.GroupId => GroupId;

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
            InitAgentData();
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

            IAdvertisementBroadcaster advertisementBroadcaster = Advertiser.GetBroadcaster();
            if (advertisementBroadcaster != null)
            {
                advertisementBroadcaster.RemoveReceiver(this);
            }

            if (stateMachine != null)
            {
                stateMachine.Destroy();
            }
        }

        protected bool isDrawingGizmos = true;

        void OnDrawGizmos()
        {
            if (isDrawingGizmos == false) return;

            IAgent agent = this as IAgent;
            if (agent.TargetAdvertisement != null)
            {
                DrawGizmosUtil.DrawTargetLocationLine(this, Color.blue);
            }

            if (data != null && Map != null)
            {
                float broadcastDistance = (data as IAgentData).BroadcastDistance * Map.CellSize.x;
                DrawGizmosUtil.DrawBroadcastDistanceSphere(Position, broadcastDistance, Color.green);
            }
        }

        void OnDrawGizmosSelected()
        {
            if (data != null && Map != null)
            {
                float broadcastDistance = (data as IAgentData).BroadcastDistance * Map.CellSize.x;
                DrawGizmosUtil.DrawBroadcastDistanceWireSphere(Position, broadcastDistance, Color.green);
            }
        }
    }
}
