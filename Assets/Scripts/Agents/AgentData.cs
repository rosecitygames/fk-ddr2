using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Advertisements;
using RCG.Attributes;

namespace RCG.Agents
{
    [System.Serializable]
    public class AgentData : IAgentData
    {
        [SerializeField]
        string displayName = "";
        string IDescribable.DisplayName
        {
            get
            {
                return displayName;
            }
        }

        [SerializeField]
        [TextArea]
        string description = "";
        string IDescribable.Description
        {
            get
            {
                return description;
            }
        }

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
        IAttributeCollection Stats
        {
            get
            {
                return stats as IAttributeCollection;
            }
        }

        List<IAttribute> IStatsCollection.Stats
        {
            get
            {
                return Stats.Attributes;
            }
        }

        IAttribute IStatsCollection.GetStat(string id)
        {
            return Stats.GetAttribute(id);
        }

        [SerializeField]
        AttributeCollection desires = new AttributeCollection();
        IAttributeCollection Desires
        {
            get
            {
                return desires as IAttributeCollection;
            }
        }

        List<IAttribute> IDesiresCollection.Desires
        {
            get
            {
                return Desires.Attributes;
            }
        }

        IAttribute IDesiresCollection.GetDesire(string id)
        {
            return Desires.GetAttribute(id);
        }

        IAgentData IAgentData.Copy()
        {
            return new AgentData(this);
        }

        public static IAgentData Create(IAgentData source)
        {
            if (source == null)
            {
                return new AgentData();
            }
            return new AgentData(source);
        }

        public static IAgentData Create()
        {
            return new AgentData();
        }

        AgentData(IAgentData source)
        {
            displayName = source.DisplayName;
            description = source.Description;
            stats = new AttributeCollection(source.Stats);
            desires = new AttributeCollection(source.Desires);
            broadcastDistance = source.BroadcastDistance;
            broadcastInterval = source.BroadcastInterval;
        }

        AgentData() { }
    }
}
