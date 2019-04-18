using System.Collections.Generic;
using UnityEngine;

namespace RCG
{
    public class Agent : MonoBehaviour, IDescribable, IStatsCollection, IDesiresCollection, ILocatable
    {
        [SerializeField]
        AgentData data = null;
        IAgentData nullData = new NullAgentData();
        IAgentData Data { get { return data ?? nullData; } }

        string IDescribable.DisplayName { get { return (Data as IDescribable).DisplayName; } }
        string IDescribable.Description { get { return (Data as IDescribable).Description; } }

        List<IAttribute> IStatsCollection.Stats { get { return (Data as IStatsCollection).Stats; } }
        IAttribute IStatsCollection.GetStat(string id) { return (Data as IStatsCollection).GetStat(id); }

        List<IAttribute> IDesiresCollection.Desires { get { return (Data as IDesiresCollection).Desires; } }
        IAttribute IDesiresCollection.GetDesire(string id) { return (Data as IDesiresCollection).GetDesire(id); }

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
