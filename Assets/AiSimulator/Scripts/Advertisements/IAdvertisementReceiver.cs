using IndieDevTools.Maps;

namespace IndieDevTools.Advertisements
{
    public interface IAdvertisementReceiver: IMapElement
    {
        void ReceiveAdvertisement(IAdvertisement advertisement);
    }
}

