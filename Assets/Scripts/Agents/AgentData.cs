﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG
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
        AttributeCollection stats = new AttributeCollection();
        IAttributable Stats
        {
            get
            {
                return stats as IAttributable;
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
        IAttributable Desires
        {
            get
            {
                return desires as IAttributable;
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

        public AgentData(IAgentData source)
        {
            displayName = source.DisplayName;
            description = source.Description;
            stats = new AttributeCollection(source.Stats);
            desires = new AttributeCollection(source.Desires);
        }

        public AgentData() { }
    }
}
