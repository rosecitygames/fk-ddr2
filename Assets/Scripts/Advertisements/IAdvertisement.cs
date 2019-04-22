using RCG.Attributes;

namespace RCG.Advertisements
{
    public interface IAdvertisement : IAttributeCollection, ILocatable
    {
        float BroadcastDistance { get; }
    }
}