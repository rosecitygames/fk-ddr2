using System.Collections.Generic;
using UnityEngine;
using RCG.Advertisements;
using RCG.States;

namespace RCG.Agents
{
    public abstract class AbstractAgent : MonoBehaviour, IDescribable, IStatsCollection, IDesiresCollection, ILocatable
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

        protected IAdvertiser advertiser = null;

        void InitAdvertiser()
        {
            return;
            //advertiser = advertiser.Create();
            advertiser.SignalStrength = StatsAgentData.GetStat("signalStrength").Quantity;
            advertiser.SignalRate = StatsAgentData.GetStat("signalRate").Quantity;
            advertiser.SignalDecay = StatsAgentData.GetStat("signalDecay").Quantity;
        }

        protected IStateMachine stateMachine = StateMachine.Create();

        protected virtual void InitStateMachine() { }

    }
}
