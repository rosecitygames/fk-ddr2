using IndieDevTools.Advertisements;
using IndieDevTools.Traits;

namespace IndieDevTools.Agents
{
    public interface IAgentData : ICopyable<IAgentData>, IDescribable, IStatsCollection, IDesiresCollection, IAdvertisementBroadcastData { }
}
