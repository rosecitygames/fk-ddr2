﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Advertisements
{
    public interface IAdvertiser
    {
        void SetBroadcaster(IAdvertisementBroadcaster broadcaster);
        void BroadcastAdvertisement(IAdvertisement advertisement);
    }
}

