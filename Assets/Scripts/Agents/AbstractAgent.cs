using System;
using System.Collections.Generic;
using UnityEngine;
using RCG.Advertisements;
using RCG.Attributes;
using RCG.Maps;
using RCG.States;

namespace RCG.Agents
{
    public abstract class AbstractAgent : MonoBehaviour, IAgent
    {
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

        string IDescribable.DisplayName { get { return AgentData.DisplayName; } }
        string IDescribable.Description { get { return AgentData.Description; } }

        List<IAttribute> IStatsCollection.Stats { get { return AgentData.Stats; } }
        IAttribute IStatsCollection.GetStat(string id) { return AgentData.GetStat(id); }

        List<IAttribute> IDesiresCollection.Desires { get { return AgentData.Desires; } }
        IAttribute IDesiresCollection.GetDesire(string id) { return AgentData.GetDesire(id); }

        [SerializeField]
        ScriptableAdvertisementBroadcaster broadcaster = null;

        [SerializeField]
        float broadcastDistance;
        float IAdvertisementBroadcastData.BroadcastDistance { get { return AgentData.BroadcastDistance; } }

        [SerializeField]
        float broadcastInterval;
        float IAdvertisementBroadcastData.BroadcastInterval { get { return AgentData.BroadcastInterval; } }

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

        void IAdvertiser.SetBroadcaster(IAdvertisementBroadcaster broadcaster)
        {
            advertiser.SetBroadcaster(broadcaster);
        }

        void IAdvertiser.BroadcastAdvertisement(IAdvertisement advertisement)
        {
            advertiser.BroadcastAdvertisement(advertisement);
        }

        void IAdvertisementReceiver.Receive(IAdvertisement advertisement)
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

        protected IStateMachine stateMachine = StateMachine.Create();

        void Start()
        {
            Init();
        }

        void Init()
        {
            InitAgentData();
            InitAdvertiser();
            InitStateMachine();
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

        protected virtual void InitStateMachine() { }
    }
}
