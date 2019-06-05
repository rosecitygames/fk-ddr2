using RCG.Maps;

namespace RCG.Advertisements
{
    public interface IAdvertisementReceiver: IMapElement
    {
        void ReceiveAdvertisement(IAdvertisement advertisement);
    }
}

