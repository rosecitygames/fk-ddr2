using RCG.Advertisements;
using RCG.Attributes;
using RCG.Maps;
using RCG.States;
using System;
using UnityEngine;

namespace RCG.Advertisements
{
    public interface IAdvertisingMapElement : IMapElement, IAdvertiser, IAdvertisementBroadcastData
    {

    }
}
