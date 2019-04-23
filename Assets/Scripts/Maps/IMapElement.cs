using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;

namespace RCG.Maps
{
    public interface IMapElement : ILocatable, IDescribable, IStatsCollection
    {
        IMap Map { get; }
        void AddToMap(IMap map);
        void RemoveFromMap();
        float Distance(IMapElement otherMapElement);
    }
}
