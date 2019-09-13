using IndieDevTools.Advertisements;
using IndieDevTools.Attributes;
using System.Collections.Generic;

namespace IndieDevTools.Items
{
    public class NullItemData : IItemData
    {
        string IDescribable.DisplayName { get => ""; }
        string IDescribable.Description { get => ""; }

        AttributeCollection stats = new AttributeCollection();
        IStatsCollection statsCollection { get => stats as IStatsCollection; }
        List<IAttribute> IStatsCollection.Stats { get => statsCollection.Stats; }
        IAttribute IStatsCollection.GetStat(string id) { return statsCollection.GetStat(id); }
        float IAdvertisementBroadcastData.BroadcastDistance { get => 0; }
        float IAdvertisementBroadcastData.BroadcastInterval { get => 0; }

        IItemData IItemData.Copy() { return new NullItemData(); }
    }
}
