using RCG.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Maps
{
    public abstract class AbstractMap : MonoBehaviour, IMap
    {
        Vector3Int IMap.Size => Size;
        protected virtual Vector3Int Size { get; }

        Vector3 IMap.CellSize => CellSize;
        protected virtual Vector3 CellSize { get; }

        int IMap.CellCount => CellCount;
        protected virtual int CellCount => Size.x * Size.y;

        Vector3Int IMap.LocalToCell(Vector3 localPosition) => LocalToCell(localPosition);
        protected virtual Vector3Int LocalToCell(Vector3 localPosition) => Vector3Int.zero;

        Vector3 IMap.CellToLocal(Vector3Int cellPosition) => CellToLocal(cellPosition);
        protected virtual Vector3 CellToLocal(Vector3Int cellPosition) => Vector3.zero;

        int IMap.CellToSortingOrder(Vector3Int cellPosition) => CellToSortingOrder(cellPosition);
        protected virtual int CellToSortingOrder(Vector3Int cellPosition) => 0;

        void IMap.AddElement(IMapElement element) => AddElement(element);
        protected virtual void AddElement(IMapElement element) { }

        void IMap.RemoveElement(IMapElement element) => RemoveElement(element);
        protected virtual void RemoveElement(IMapElement element) { }

        List<IMapElement> IMap.GetMapElementsAtCells(List<Vector3Int> cells) => GetMapElementsAtCells(cells);
        protected virtual List<IMapElement> GetMapElementsAtCells(List<Vector3Int> cells) => new List<IMapElement>();

        List<IMapElement> IMap.GetMapElementsAtCell(Vector3Int cell) => GetMapElementsAtCell(cell);
        protected virtual List<IMapElement> GetMapElementsAtCell(Vector3Int cell) => new List<IMapElement>();

        string IDescribable.DisplayName => DisplayName;
        protected virtual string DisplayName { get; }

        string IDescribable.Description => Description;
        protected virtual string Description { get; }
    }
}
