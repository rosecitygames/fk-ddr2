using System.Collections.Generic;

namespace IndieDevTools.Traits
{
    public interface IDesiresCollection
    {
        List<ITrait> Desires { get; }
        ITrait GetDesire(string id);
    }
}
