using System.Collections.Generic;

namespace IndieDevTools.Traits
{
    public interface IStatsCollection
    {
        List<ITrait> Stats { get; }
        ITrait GetStat(string id);
    }
}
