using IndieDevTools.Advertisements;
using IndieDevTools.Attributes;
using IndieDevTools.Common;

namespace IndieDevTools.Items
{
    public interface IItemData : ICopyable<IItemData>, IDescribable, IStatsCollection, IAdvertisementBroadcastData { }
}