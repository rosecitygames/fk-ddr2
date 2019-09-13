using System.Collections.Generic;

namespace IndieDevTools.Attributes
{
    public interface IStatsCollection
    {
        List<IAttribute> Stats { get; }
        IAttribute GetStat(string id);
    }
}
