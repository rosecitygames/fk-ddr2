using IndieDevTools.Advertisements;
using IndieDevTools.Traits;

namespace IndieDevTools.Items
{
    public interface IItemData : ICopyable<IItemData>, IDescribable, IStatsCollection, IAdvertisementBroadcastData { }
}