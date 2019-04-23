﻿using RCG.Maps;

namespace RCG.Advertisements
{
    public interface IAdvertisementReceiver: ILocatable
    {
        void Receive(IAdvertisement advertisement);
    }
}

