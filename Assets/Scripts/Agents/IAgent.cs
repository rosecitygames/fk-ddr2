using RCG.Advertisements;
using RCG.Attributes;
using RCG.Maps;
using System;

namespace RCG.Agents
{
    public interface IAgent : IDescribable, IStatsCollection, IDesiresCollection, ILocatable, IAdvertiser, IAdvertisementReceiver, IAdvertisementBroadcastData
    {
        IAgentData AgentData { get; set; }
        event Action<IAdvertisement> OnAdvertisementReceived;
        IRankedAdvertisement TargetAdvertisement { get; set; }
    }
}

