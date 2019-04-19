﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Agents
{
    [CreateAssetMenu(fileName = "AgentData", menuName = "RCG/Agent Data")]
    public class ScriptableAgentData : ScriptableObject, IAgentData
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
    }
}
