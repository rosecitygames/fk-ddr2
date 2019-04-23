using RCG.Attributes;
using RCG.Maps;

namespace RCG.Advertisements
{
    public interface IAdvertisement : IAttributeCollection, ILocatable, IGroupMember
    {
        float BroadcastDistance { get; }
    }
}