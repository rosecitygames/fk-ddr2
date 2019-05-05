using RCG.Advertisements;
using RCG.Attributes;
using RCG.Maps;
using RCG.States;
using RCG.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Agents
{
    public abstract class AbstractAgent : MonoBehaviour, IAgent
    {
        // Agent Data implementations
        [SerializeField]
        ScriptableAgentData data = null;
        IAgentData agentData;

        IAgentData IAgent.AgentData
        {
            get
            {
                return AgentData;
            }
            set
            {
                AgentData = value;
            }
        }
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

        string IDescribable.DisplayName { get => AgentData.DisplayName; }
        string IDescribable.Description { get => AgentData.Description; }

        List<IAttribute> IStatsCollection.Stats { get => AgentData.Stats; }
        IAttribute IStatsCollection.GetStat(string id) { return AgentData.GetStat(id); }

        List<IAttribute> IDesiresCollection.Desires { get => AgentData.Desires; }
        IAttribute IDesiresCollection.GetDesire(string id) { return AgentData.GetDesire(id); }

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
            return Vector3Int.Distance(otherMapElement.Location, Location);
        }

        int IMapElement.SortingOrder { get => SortingOrder; }
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

        // Broadcaster & Advertiser implementations
        [SerializeField]
        ScriptableAdvertisementBroadcaster broadcaster = null;

        float IAdvertisementBroadcastData.BroadcastDistance { get => BroadcastDistance; }
        protected float BroadcastDistance { get => AgentData.BroadcastDistance; }

        float IAdvertisementBroadcastData.BroadcastInterval { get => BroadcastInterval; }
        protected float BroadcastInterval { get => AgentData.BroadcastInterval; }

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

        Vector3Int IAgent.TargetLocation { get; set; }

        // Group Member implementations
        [SerializeField]
        int groupId;
        protected int GroupId { get => groupId; set => groupId = value; }
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
                float broadcastDistance = (data as IAgentData).BroadcastDistance * 0.2f;
                DrawGizmosUtil.DrawBroadcastDistanceSphere(Position, broadcastDistance, Color.green);
            }
        }
    }
}
