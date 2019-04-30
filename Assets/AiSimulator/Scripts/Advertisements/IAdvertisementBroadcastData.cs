using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Advertisements
{
    public interface IAdvertisementBroadcastData
    {
        float BroadcastDistance { get; }
        float BroadcastInterval { get; }
    }
}
