using RCG.Advertisements;
using RCG.Attributes;
using System.Collections.Generic;

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

        float IAdvertisementBroadcastData.BroadcastDistance { get { return 0; } }
        float IAdvertisementBroadcastData.BroadcastInterval { get { return 0; } }

        IAgentData IAgentData.Copy() { return new NullAgentData(); }
    }
}
