using System.Collections.Generic;

namespace RCG.Paths
{
    public interface IWeightedGraph<T>
    {
        int Cost(T a, T b);
        IEnumerable<T> Neighbors(T id);
    }
}
