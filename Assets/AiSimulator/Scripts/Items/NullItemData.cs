using IndieDevTools.Advertisements;
using IndieDevTools.Attributes;
using System;
using System.Collections.Generic;

namespace IndieDevTools.Items
{
    public class NullItemData : IItemData
    {
        string IDescribable.DisplayName { get => ""; set { } }
        string IDescribable.Description { get => ""; set { } }
        event Action<IDescribable> IUpdatable<IDescribable>.OnUpdated { add { } remove { } }

        AttributeCollection stats = new AttributeCollection();
        IStatsCollection statsCollection { get => stats as IStatsCollection; }
        List<IAttribute> IStatsCollection.Stats { get => statsCollection.Stats; }
        IAttribute IStatsCollection.GetStat(string id) { return statsCollection.GetStat(id); }
        float IAdvertisementBroadcastData.BroadcastDistance { get => 0; }
        float IAdvertisementBroadcastData.BroadcastInterval { get => 0; }

        IItemData ICopyable<IItemData>.Copy() { return new NullItemData(); }
    }
}
