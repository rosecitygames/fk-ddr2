using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;

namespace RCG.Maps
{
    public interface IMap : IDescribable
    {
        float CellSize { get; }
        void AddElement(IMapElement element);
        void RemoveElement(IMapElement element);
    }
}
