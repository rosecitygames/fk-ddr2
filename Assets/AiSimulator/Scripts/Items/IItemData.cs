using RCG.Advertisements;
using RCG.Attributes;

namespace RCG.Items
{
    public interface IItemData : IDescribable, IStatsCollection, IAdvertisementBroadcastData
    {
        IItemData Copy();
    }
}