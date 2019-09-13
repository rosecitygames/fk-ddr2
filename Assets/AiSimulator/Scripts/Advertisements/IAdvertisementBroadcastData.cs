using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Advertisements
{
    public interface IAdvertisementBroadcastData
    {
        float BroadcastDistance { get; }
        float BroadcastInterval { get; }
    }
}
