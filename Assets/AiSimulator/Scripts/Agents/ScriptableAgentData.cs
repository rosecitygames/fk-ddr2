using IndieDevTools.Advertisements;
using IndieDevTools.Traits;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Agents
{
    [CreateAssetMenu(fileName = "AgentData", menuName = "IndieDevTools/Agent Data")]
    public class ScriptableAgentData : ScriptableObject, IAgentData
    {
        [SerializeField]
        AgentData data = null;
        IAgentData runtimeData = null;
        IAgentData RuntimeData
        {
            get
            {
                if (runtimeData == null)
                {
                    if (data == null)
                    {
                        runtimeData = NullAgentData.Create();
                    }
                    else
                    {
                        runtimeData = (data as IAgentData).Copy();
                    }
                }

                return runtimeData;
            }
        }

        string IDescribable.DisplayName
        {
            get => RuntimeData.DisplayName;
            set => RuntimeData.DisplayName = value;
        }

        string IDescribable.Description
        {
            get => RuntimeData.Description;
            set => RuntimeData.Description = value;
        }

        event Action<IDescribable> IUpdatable<IDescribable>.OnUpdated
        {
            add { (RuntimeData as IDescribable).OnUpdated += value; }
            remove { (RuntimeData as IDescribable).OnUpdated -= value; }
        }

        float IAdvertisementBroadcastData.BroadcastDistance => RuntimeData.BroadcastDistance;

        float IAdvertisementBroadcastData.BroadcastInterval => RuntimeData.BroadcastInterval;

        List<ITrait> IStatsCollection.Stats => RuntimeData.Stats;
        ITrait IStatsCollection.GetStat(string id) => RuntimeData.GetStat(id);

        List<ITrait> IDesiresCollection.Desires => RuntimeData.Desires;
        ITrait IDesiresCollection.GetDesire(string id) => RuntimeData.GetDesire(id);

        IAgentData ICopyable<IAgentData>.Copy() => RuntimeData.Copy();
    }
}
