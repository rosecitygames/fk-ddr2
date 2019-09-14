using IndieDevTools.Advertisements;
using IndieDevTools.Attributes;

namespace IndieDevTools.Items
{
    public interface IItemData : ICopyable<IItemData>, IDescribable, IStatsCollection, IAdvertisementBroadcastData { }
}