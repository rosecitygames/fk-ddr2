using IndieDevTools.Advertisements;
using IndieDevTools.Attributes;

namespace IndieDevTools.Items
{
    public interface IItemData : IDescribable, IStatsCollection, IAdvertisementBroadcastData
    {
        IItemData Copy();
    }
}