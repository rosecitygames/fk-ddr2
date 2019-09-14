using IndieDevTools.Advertisements;
using IndieDevTools.Attributes;
using IndieDevTools.Common;
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
        string IDescribable.DisplayName { get => displayName; set => displayName = value; }

        [SerializeField]
        [TextArea]
        string description = "";
        string IDescribable.Description { get => description; set => description = value; }

        [SerializeField]
        float broadcastDistance = 0.0f;
        float IAdvertisementBroadcastData.BroadcastDistance
        {
            get
            {
                return broadcastDistance;
            }
        }

        [SerializeField]
        float broadcastInterval = 0.0f;
        float IAdvertisementBroadcastData.BroadcastInterval
        {
            get
            {
                return broadcastInterval;
            }
        }

        [SerializeField]
        AttributeCollection stats = new AttributeCollection();
        IAttributeCollection iStats = null;
        IAttributeCollection Stats
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

        List<IAttribute> IStatsCollection.Stats => Stats.Attributes;
        IAttribute IStatsCollection.GetStat(string id) => Stats.GetAttribute(id);

        [SerializeField]
        AttributeCollection desires = new AttributeCollection();
        IAttributeCollection iDesires = null;
        IAttributeCollection Desires
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

        List<IAttribute> IDesiresCollection.Desires => Desires.Attributes;

        IAttribute IDesiresCollection.GetDesire(string id) => Desires.GetAttribute(id);

        IAgentData ICopyable<IAgentData>.Copy()
        {
            return Create(this);
        }

        public static IAgentData Create(IAgentData source)
        {
            return new AgentData
            {
                displayName = source.DisplayName,
                description = source.Description,
                iStats = AttributeCollection.Create(source.Stats),
                iDesires = AttributeCollection.Create(source.Desires),
                broadcastDistance = source.BroadcastDistance,
                broadcastInterval = source.BroadcastInterval
            };
        }

        public static IAgentData Create()
        {
            return new AgentData();
        }
                
    }
}
