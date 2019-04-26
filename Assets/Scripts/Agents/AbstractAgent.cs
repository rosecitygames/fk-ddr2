using RCG.Advertisements;
using RCG.Attributes;
using RCG.Maps;
using RCG.States;
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
                InitAgentData();
                return agentData;
            }
            set
            {
                agentData = value;
            }
        }

        void InitAgentData()
        {
            if (agentData == null)
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
        }

        string IDescribable.DisplayName { get { return AgentData.DisplayName; } }
        string IDescribable.Description { get { return AgentData.Description; } }

        List<IAttribute> IStatsCollection.Stats { get { return AgentData.Stats; } }
        IAttribute IStatsCollection.GetStat(string id) { return AgentData.GetStat(id); }

        List<IAttribute> IDesiresCollection.Desires { get { return AgentData.Desires; } }
        IAttribute IDesiresCollection.GetDesire(string id) { return AgentData.GetDesire(id); }

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

        // Broadcaster & Advertiser implementations
        [SerializeField]
        ScriptableAdvertisementBroadcaster broadcaster = null;

        float IAdvertisementBroadcastData.BroadcastDistance { get { return BroadcastDistance; } }
        protected float BroadcastDistance { get { return AgentData.BroadcastDistance; } }

        float IAdvertisementBroadcastData.BroadcastInterval { get { return BroadcastInterval; } }
        protected float BroadcastInterval { get { return AgentData.BroadcastInterval; } }

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
                if (broadcaster != null)
                {
                    (broadcaster as IAdvertisementBroadcaster).AddReceiver(this);
                }
                advertiser = Advertisements.Advertiser.Create(broadcaster);
            }
        }

        void IAdvertiser.SetBroadcaster(IAdvertisementBroadcaster broadcaster)
        {
            advertiser.SetBroadcaster(broadcaster);
        }

        void IAdvertiser.BroadcastAdvertisement(IAdvertisement advertisement)
        {
            advertiser.BroadcastAdvertisement(advertisement);
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

        IAgent IAgent.TargetAgent { get; set; }

        Vector3Int IAgent.TargetLocation { get; set; }

        // Group Member implementations
        [SerializeField]
        int groupId;
        protected int GroupId { get { return groupId; } set { groupId = value; } }
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

        void OnDestroy()
        {
            if (stateMachine != null)
            {
                stateMachine.Destroy();
            }
        }

        // Initialization
        void Start()
        {
            Init();
        }

        void Init()
        {
            InitAgentData();
            InitAdvertiser();
            InitMap();
            InitStateMachine();
        }

        protected void DrawLineToTargetAdvertisementGizmo(Color color)
        {
            IAgent agent = this as IAgent;
            if (agent.TargetAdvertisement != null)
            {
                Gizmos.color = color;
                Gizmos.DrawLine(transform.localPosition, Map.CellToLocal(agent.TargetAdvertisement.Location));
            }
        }

        protected void DrawBroadcastDistanceGizmo(Color baseColor)
        {
            if (data == null) return;

            float broadcastDistance = (data as IAgentData).BroadcastDistance * 0.2f;

            Color gizmoColor = baseColor;

            gizmoColor.a = baseColor.a * 0.2f;
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, broadcastDistance);

            gizmoColor.a = baseColor.a * 0.1f;
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, broadcastDistance);
        }
    }
}
