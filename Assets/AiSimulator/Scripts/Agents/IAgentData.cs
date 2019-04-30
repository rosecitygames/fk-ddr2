using RCG.Advertisements;
using RCG.Attributes;

namespace RCG.Agents
{
    public interface IAgentData : IDescribable, IStatsCollection, IDesiresCollection, IAdvertisementBroadcastData
    {
        IAgentData Copy();
    }
}
