using RCG.Advertisements;

namespace RCG.Items
{
    public interface IItem : ILocatable, IAdvertiser, IAdvertisementBroadcastData
    {
        IItemData ItemData { get; set; }
    }
}
