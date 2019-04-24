using RCG.Advertisements;
using RCG.Attributes;
using RCG.Maps;

namespace RCG.Items
{
    public interface IItem : IMapElement, IGroupMember, IAdvertiser, IAdvertisementBroadcastData
    {
        IItemData ItemData { get; set; }
    }
}
