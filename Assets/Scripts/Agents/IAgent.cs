using RCG.Advertisements;
using System;

namespace RCG.Agents
{
    public interface IAgent : IDescribable, IStatsCollection, IDesiresCollection, ILocatable, IAdvertiser, IAdvertisementReceiver
    {
        IAgentData AgentData { get; set; }
        event Action<IAdvertisement> OnAdvertisementReceived;
    }
}

