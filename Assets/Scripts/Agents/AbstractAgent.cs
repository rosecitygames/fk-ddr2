using System;
using System.Collections.Generic;
using UnityEngine;
using RCG.Advertisements;
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

        protected IDescribable DescribableAgentData { get { return AgentData as IDescribable; } }
        string IDescribable.DisplayName { get { return DescribableAgentData.DisplayName; } }
        string IDescribable.Description { get { return DescribableAgentData.Description; } }

        protected IStatsCollection StatsAgentData { get { return AgentData as IStatsCollection; } }
        List<IAttribute> IStatsCollection.Stats { get { return StatsAgentData.Stats; } }
        IAttribute IStatsCollection.GetStat(string id) { return StatsAgentData.GetStat(id); }

        protected IDesiresCollection DesiresAgentData { get { return AgentData as IDesiresCollection; } }
        List<IAttribute> IDesiresCollection.Desires { get { return DesiresAgentData.Desires; } }
        IAttribute IDesiresCollection.GetDesire(string id) { return DesiresAgentData.GetDesire(id); }

        [SerializeField]
        ScriptableAdvertisementBroadcaster broadcaster;

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
                advertiser = Advertisements.Advertiser.Create(broadcaster);
            }
        }

        protected virtual void InitStateMachine() { }
    }
}
