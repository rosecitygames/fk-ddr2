using RCG.Attributes;
using RCG.Maps;

namespace RCG.Advertisements
{
    public interface IAdvertisement : IAttributeCollection, ILocatable
    {
        float BroadcastDistance { get; }
    }
}