using System.Collections.Generic;
using UnityEngine;
using RCG.Advertisements;
using RCG.States;

namespace RCG.Agents
{
    public class Agent : MonoBehaviour, IDescribable, IStatsCollection, IDesiresCollection, ILocatable, IAdvertisementHandler
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

        IDescribable DescribableAgentData { get { return AgentData as IDescribable; } }
        string IDescribable.DisplayName { get { return DescribableAgentData.DisplayName; } }
        string IDescribable.Description { get { return DescribableAgentData.Description; } }

        IStatsCollection StatsAgentData { get { return AgentData as IStatsCollection; } }
        List<IAttribute> IStatsCollection.Stats { get { return StatsAgentData.Stats; } }
        IAttribute IStatsCollection.GetStat(string id) { return StatsAgentData.GetStat(id); }

        IDesiresCollection DesiresAgentData { get { return AgentData as IDesiresCollection; } }
        List<IAttribute> IDesiresCollection.Desires { get { return DesiresAgentData.Desires; } }
        IAttribute IDesiresCollection.GetDesire(string id) { return DesiresAgentData.GetDesire(id); }

        Vector2 ILocatable.Location
        {
            get
            {
                return transform.position; // TODO: Eventually maps to map grid
            }
        }

        void Start()
        {
            Init();
        }


        void Init()
        {
            InitAgentData();
            InitStateMachine();
            //InitAdvertiser();
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

        IStateMachine stateMachine;

        void InitStateMachine()
        {
            stateMachine = StateMachine.Create();

            /*
            IState initState = PlayerControllerState.Create(PlayerControllerStateType.Init, this);
		    initState.AddTransition(PlayerControllerStateEventType.ActionComplete.ToString(), PlayerControllerStateType.Default.ToString());
		    initState.actionPlayer.AddAction(PlayerControllerNPCInitAction.Create(this));
		    initState.actionPlayer.AddAction(PauseAction.Create(1.5f));
		    initState.actionPlayer.AddAction( CallEventAction.Create(initState, PlayerAnimatorStateEventType.ActionComplete.ToString()) );
		    AddState(initState); 
            */

            ActionableState roamingState = ActionableState.Create("roaming");
            //roamingState.AddAction(RoamingAction.Create());
            stateMachine.AddState(roamingState);
        }

        void IAdvertisementHandler.HandleAdvertisement(IAdvertisement advertisement)
        {
            //stateMachine.currentState.HandleAdvertisement(advertisement);
        }

        IAdvertiser advertiser = null;

        void InitAdvertiser()
        {
            //advertiser = advertiser.Create();
            advertiser.SignalStrength = StatsAgentData.GetStat("signalStrength").Quantity;
            advertiser.SignalRate = StatsAgentData.GetStat("signalRate").Quantity;
            advertiser.SignalDecay = StatsAgentData.GetStat("signalDecay").Quantity;
        }
    }
}
