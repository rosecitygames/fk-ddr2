using IndieDevTools.Advertisements;
using IndieDevTools.Attributes;
using IndieDevTools.Common;

namespace IndieDevTools.Items
{
    public interface IItemData : IDescribable, IStatsCollection, IAdvertisementBroadcastData
    {
        IItemData Copy();
    }
}