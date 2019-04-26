using RCG.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Maps
{
    public interface IMap : IDescribable
    {
        Vector3 CellSize { get; }

        Vector3Int LocalToCell(Vector3 localPosition);
        Vector3 CellToLocal(Vector3Int cellPosition);

        void AddElement(IMapElement element);
        void RemoveElement(IMapElement element);

        List<IMapElement> GetMapElementsAtCell(Vector3Int cell);
    }
}
