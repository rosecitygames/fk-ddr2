using System.Collections.Generic;

namespace RCG.Attributes
{
    public interface IDesiresCollection
    {
        List<IAttribute> Desires { get; }
        IAttribute GetDesire(string id);
    }
}
