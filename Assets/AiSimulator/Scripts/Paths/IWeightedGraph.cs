using System.Collections.Generic;

namespace IndieDevTools.Paths
{
    public interface IWeightedGraph<T>
    {
        int Cost(T a, T b);
        IEnumerable<T> Neighbors(T id);
    }
}
