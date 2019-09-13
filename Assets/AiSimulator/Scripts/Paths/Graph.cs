using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Paths
{
    public class Graph<T>
    {
        public Dictionary<T, T[]> Edges = new Dictionary<T, T[]>();

        public T[] Neighbors(T id)
        {
            return Edges[id];
        }
    }
}
