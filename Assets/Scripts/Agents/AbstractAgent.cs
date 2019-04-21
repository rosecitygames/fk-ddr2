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
        public IAgentData AgentData
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

        protected IAdvertiser advertiser = Advertiser.Create();

        void IAdvertiser.PublishAdvertisement(IAdvertisement advertisement)
        {
            advertiser.PublishAdvertisement(advertisement);
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

        protected IStateMachine stateMachine = StateMachine.Create();

        void Start()
        {
            Init();
        }

        void Init()
        {
            InitAgentData();
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

        protected virtual void InitStateMachine() { }
    }
}
