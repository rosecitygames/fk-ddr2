﻿using RCG.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Maps
{
    public class NullMap : IMap
    {
        Vector3Int IMap.Size { get { return Vector3Int.zero; } }

        Vector3 IMap.CellSize { get { return Vector3.zero; } }

        Vector3Int IMap.LocalToCell(Vector3 localPosition) { return Vector3Int.zero; }
        Vector3 IMap.CellToLocal(Vector3Int cellPosition) { return Vector3.zero; }
        int IMap.CellToSortingOrder(Vector3Int cellPosition) { return 0; }

        void IMap.AddElement(IMapElement element) { }
        void IMap.RemoveElement(IMapElement element) { }
        List<IMapElement> IMap.GetMapElementsAtCell(Vector3Int cell) { return new List<IMapElement>(); }

        string IDescribable.DisplayName { get; }
        string IDescribable.Description { get; }

        public static IMap Create()
        {
            return new NullMap();
        }
    }
}
