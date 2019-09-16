using IndieDevTools.Advertisements;
using IndieDevTools.Traits;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Agents
{
    [Serializable]
    public class AgentData : IAgentData
    {
        [SerializeField]
        string displayName = "";
        string IDescribable.DisplayName
        {
            get => displayName;
            set
            {
                if (displayName != value)
                {
                    displayName = value;
                    OnDescribableUpdated?.Invoke(this);
                }
            }
        }

        [SerializeField, TextArea]
        string description = "";
        string IDescribable.Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    OnDescribableUpdated?.Invoke(this);
                }
            }
        }

        event Action<IDescribable> IUpdatable<IDescribable>.OnUpdated { add { OnDescribableUpdated += value; } remove { OnDescribableUpdated -= value; } }
        Action<IDescribable> OnDescribableUpdated;

        [SerializeField]
        float broadcastDistance = 0.0f;
        float IAdvertisementBroadcastData.BroadcastDistance => broadcastDistance;

        [SerializeField]
        float broadcastInterval = 0.0f;
        float IAdvertisementBroadcastData.BroadcastInterval => broadcastInterval;

        [SerializeField]
        TraitCollection stats = new TraitCollection();
        ITraitCollection iStats = null;
        ITraitCollection Stats
        {
            get
            {
                if (iStats == null)
                {
                    iStats = stats;
                }
                return iStats;
            }
        }

        List<ITrait> IStatsCollection.Stats => Stats.Traits;
        ITrait IStatsCollection.GetStat(string id) => Stats.GetTrait(id);

        [SerializeField]
        TraitCollection desires = new TraitCollection();
        ITraitCollection iDesires = null;
        ITraitCollection Desires
        {
            get
            {
                if (iDesires == null)
                {
                    iDesires = desires;
                }
                return iDesires;
            }
        }

        List<ITrait> IDesiresCollection.Desires => Desires.Traits;

        ITrait IDesiresCollection.GetDesire(string id) => Desires.GetTrait(id);

        IAgentData ICopyable<IAgentData>.Copy() => Create(this);

        public static IAgentData Create(IAgentData source)
        {
            return new AgentData
            {
                displayName = source.DisplayName,
                description = source.Description,
                iStats = TraitCollection.Create(source.Stats),
                iDesires = TraitCollection.Create(source.Desires),
                broadcastDistance = source.BroadcastDistance,
                broadcastInterval = source.BroadcastInterval
            };
        }

        public static IAgentData Create() => new AgentData();
    }
}
