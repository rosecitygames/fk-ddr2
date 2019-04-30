using RCG.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Maps
{
    public abstract class AbstractMap : MonoBehaviour, IMap
    {
        Vector3Int IMap.Size { get { return Size; } }
        protected virtual Vector3Int Size { get; }

        Vector3 IMap.CellSize { get { return CellSize; } }
        protected virtual Vector3 CellSize { get; }

        Vector3Int IMap.LocalToCell(Vector3 localPosition) { return LocalToCell(localPosition); }
        protected virtual Vector3Int LocalToCell(Vector3 localPosition) { return Vector3Int.zero; }

        Vector3 IMap.CellToLocal(Vector3Int cellPosition) { return CellToLocal(cellPosition); }
        protected virtual Vector3 CellToLocal(Vector3Int cellPosition) { return Vector3.zero; }

        int IMap.CellToSortingOrder(Vector3Int cellPosition) { return CellToSortingOrder(cellPosition); }
        protected virtual int CellToSortingOrder(Vector3Int cellPosition) { return 0; }

        void IMap.AddElement(IMapElement element) { AddElement(element); }
        protected virtual void AddElement(IMapElement element) { }

        void IMap.RemoveElement(IMapElement element) { RemoveElement(element); }
        protected virtual void RemoveElement(IMapElement element) { }

        List<IMapElement> IMap.GetMapElementsAtCell(Vector3Int cell) { return GetMapElementsAtCell(cell); }
        protected virtual List<IMapElement> GetMapElementsAtCell(Vector3Int cell) { return new List<IMapElement>(); }

        string IDescribable.DisplayName { get { return DisplayName; } }
        protected virtual string DisplayName { get; }

        string IDescribable.Description { get { return Description; } }
        protected virtual string Description { get; }
    }
}
