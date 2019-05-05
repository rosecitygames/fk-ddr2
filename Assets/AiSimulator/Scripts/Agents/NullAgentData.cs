using RCG.Advertisements;
using RCG.Attributes;
using System.Collections.Generic;

namespace RCG.Agents
{
    public class NullAgentData : IAgentData
    {
        string IDescribable.DisplayName { get => ""; }
        string IDescribable.Description { get => ""; }

        IAttributeCollection stats = new AttributeCollection();
        List<IAttribute> IStatsCollection.Stats { get => stats.Attributes; }
        IAttribute IStatsCollection.GetStat(string id) { return stats.GetAttribute(id); }

        IAttributeCollection desires = new AttributeCollection();
        List<IAttribute> IDesiresCollection.Desires { get => desires.Attributes; }
        IAttribute IDesiresCollection.GetDesire(string id) { return desires.GetAttribute(id); }

        float IAdvertisementBroadcastData.BroadcastDistance { get => 0; }
        float IAdvertisementBroadcastData.BroadcastInterval { get => 0; }

        IAgentData IAgentData.Copy() { return new NullAgentData(); }
    }
}
