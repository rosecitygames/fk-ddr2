using System.Collections.Generic;

namespace RCG
{
    public interface IDesiresCollection
    {
        List<IAttribute> Desires { get; }
        IAttribute GetDesire(string id);
    }
}
