using IndieDevTools.Advertisements;
using IndieDevTools.Attributes;

namespace IndieDevTools.Agents
{
    public interface IAgentData : IDescribable, IStatsCollection, IDesiresCollection, IAdvertisementBroadcastData
    {
        IAgentData Copy();
    }
}
