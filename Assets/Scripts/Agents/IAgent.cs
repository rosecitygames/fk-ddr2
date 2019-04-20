using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Advertisements;

namespace RCG.Agents
{
    public interface IAgent : IDescribable, IStatsCollection, IDesiresCollection, ILocatable, IAdvertiser, IAdvertisementReceiver
    {
        event System.Action<IAdvertisement> OnAdvertisementReceived;
    }
}

