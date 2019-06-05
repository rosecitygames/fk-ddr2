﻿using RCG.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Maps
{
    public interface IMap : IDescribable
    {
        Vector3Int Size { get; }
        Vector3 CellSize { get; }
        int CellCount { get; }

        Vector3Int LocalToCell(Vector3 localPosition);
        Vector3 CellToLocal(Vector3Int cellPosition);
        int CellToSortingOrder(Vector3Int cellPosition);

        void AddElement(IMapElement element);
        void RemoveElement(IMapElement element);

        List<IMapElement> GetMapElementsAtCells(List<Vector3Int> cells);
        List<IMapElement> GetMapElementsAtCell(Vector3Int cell);      
    }
}
