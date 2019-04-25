using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;

namespace RCG.Maps
{
    public interface IMapElement : ILocatable, IPositionable, IDescribable, IStatsCollection, IGroupMember
    {
        IMap Map { get; set; }
        void AddToMap(IMap map);
        void RemoveFromMap();
        float Distance(IMapElement otherMapElement);     
    }
}
