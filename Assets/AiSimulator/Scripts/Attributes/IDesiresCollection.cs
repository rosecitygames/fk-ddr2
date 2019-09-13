using System.Collections.Generic;

namespace IndieDevTools.Attributes
{
    public interface IDesiresCollection
    {
        List<IAttribute> Desires { get; }
        IAttribute GetDesire(string id);
    }
}
