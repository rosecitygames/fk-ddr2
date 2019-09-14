using IndieDevTools.Advertisements;
using IndieDevTools.Attributes;

namespace IndieDevTools.Agents
{
    public interface IAgentData : ICopyable<IAgentData>, IDescribable, IStatsCollection, IDesiresCollection, IAdvertisementBroadcastData { }
}
