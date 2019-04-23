using RCG.Advertisements;
using RCG.Maps;

namespace RCG.Items
{
    public interface IItem : ILocatable, IAdvertiser, IAdvertisementBroadcastData
    {
        IItemData ItemData { get; set; }
    }
}
