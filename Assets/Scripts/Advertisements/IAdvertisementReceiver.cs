using RCG.Maps;

namespace RCG.Advertisements
{
    public interface IAdvertisementReceiver: ILocatable
    {
        void ReceiveAdvertisement(IAdvertisement advertisement);
    }
}

