using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Agents
{
    public class NullAgentData : IAgentData
    {
        string IDescribable.DisplayName { get { return ""; } }
        string IDescribable.Description { get { return ""; } }

        IAttributeCollection stats = new AttributeCollection();
        List<IAttribute> IStatsCollection.Stats { get { return stats.Attributes; } }
        IAttribute IStatsCollection.GetStat(string id) { return stats.GetAttribute(id); }

        IAttributeCollection desires = new AttributeCollection();
        List<IAttribute> IDesiresCollection.Desires { get { return desires.Attributes; } }
        IAttribute IDesiresCollection.GetDesire(string id) { return desires.GetAttribute(id); }

        IAgentData IAgentData.Copy() { return new NullAgentData(); }
    }
}
