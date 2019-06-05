using RCG.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Maps
{
    public class NullMap : IMap
    {
        Vector3Int IMap.Size => Vector3Int.zero;

        Vector3 IMap.CellSize => Vector3.zero;

        int IMap.CellCount => 0;

        Vector3Int IMap.LocalToCell(Vector3 localPosition) => Vector3Int.zero;
        Vector3 IMap.CellToLocal(Vector3Int cellPosition) => Vector3.zero;
        int IMap.CellToSortingOrder(Vector3Int cellPosition) => 0;

        void IMap.AddElement(IMapElement element) { }
        void IMap.RemoveElement(IMapElement element) { }
        List<IMapElement> IMap.GetMapElementsAtCell(Vector3Int cell) => new List<IMapElement>();

        string IDescribable.DisplayName { get; }
        string IDescribable.Description { get; }

        public static IMap Create()
        {
            return new NullMap();
        }
    }
}

