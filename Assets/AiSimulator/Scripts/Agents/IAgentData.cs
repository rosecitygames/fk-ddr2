using IndieDevTools.Advertisements;
using IndieDevTools.Attributes;
using IndieDevTools.Common;

namespace IndieDevTools.Agents
{
    public interface IAgentData : IDescribable, IStatsCollection, IDesiresCollection, IAdvertisementBroadcastData
    {
        IAgentData Copy();
    }
}
