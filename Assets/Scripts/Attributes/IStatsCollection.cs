using System.Collections.Generic;

namespace RCG
{
    public interface IStatsCollection
    {
        List<IAttribute> Stats { get; }
        IAttribute GetStat(string id);
    }
}
