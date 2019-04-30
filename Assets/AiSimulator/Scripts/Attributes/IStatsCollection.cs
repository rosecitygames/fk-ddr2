using System.Collections.Generic;

namespace RCG.Attributes
{
    public interface IStatsCollection
    {
        List<IAttribute> Stats { get; }
        IAttribute GetStat(string id);
    }
}
