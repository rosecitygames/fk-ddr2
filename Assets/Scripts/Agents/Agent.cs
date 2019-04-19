using System.Collections.Generic;
using UnityEngine;

namespace RCG
{
    public class Agent : MonoBehaviour, IDescribable, IStatsCollection, IDesiresCollection, ILocatable
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

        string IDescribable.DisplayName { get { return (AgentData as IDescribable).DisplayName; } }
        string IDescribable.Description { get { return (AgentData as IDescribable).Description; } }

        List<IAttribute> IStatsCollection.Stats { get { return (AgentData as IStatsCollection).Stats; } }
        IAttribute IStatsCollection.GetStat(string id) { return (AgentData as IStatsCollection).GetStat(id); }

        List<IAttribute> IDesiresCollection.Desires { get { return (AgentData as IDesiresCollection).Desires; } }
        IAttribute IDesiresCollection.GetDesire(string id) { return (AgentData as IDesiresCollection).GetDesire(id); }

        Vector2 ILocatable.Location
        {
            get
            {
                return transform.position; // TODO: Eventually maps to map grid
            }
        }

        // Has State Machine

        // Has Advertiser
    }
}
