using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Advertisements
{
    public interface IAdvertiser
    {
        IAdvertisement Advertisement { get; set; }
        int SignalStrength { get; set; }
        float SignalRate { get; set; }
        float SignalDecay { get; set; }
    }
}

