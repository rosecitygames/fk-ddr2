using RCG.Advertisements;
using RCG.Attributes;
using System.Collections.Generic;

namespace RCG.Items
{
    public class NullItemData : IItemData
    {
        string IDescribable.DisplayName { get { return ""; } }
        string IDescribable.Description { get { return ""; } }

        AttributeCollection stats = new AttributeCollection();
        IStatsCollection statsCollection { get { return stats as IStatsCollection; } }
        List<IAttribute> IStatsCollection.Stats { get { return statsCollection.Stats; } }
        IAttribute IStatsCollection.GetStat(string id) { return statsCollection.GetStat(id); }
        float IAdvertisementBroadcastData.BroadcastDistance { get { return 0; } }
        float IAdvertisementBroadcastData.BroadcastInterval { get { return 0; } }

        IItemData IItemData.Copy() { return new NullItemData(); }
    }
}
