using RCG.Attributes;
using System.Collections.Generic;
using UnityEngine;
using RCG.Paths;

namespace RCG.Maps
{
    public abstract class AbstractMap : MonoBehaviour, IMap
    {
        Vector2Int IMap.Size => Size;
        protected virtual Vector2Int Size { get; }

        Vector3 IMap.CellSize => CellSize;
        protected virtual Vector3 CellSize { get; }

        int IMap.CellCount => CellCount;
        protected virtual int CellCount => Size.x * Size.y;

        Vector2Int IMap.LocalToCell(Vector3 localPosition) => LocalToCell(localPosition);
        protected virtual Vector2Int LocalToCell(Vector3 localPosition) => Vector2Int.zero;

        Vector3 IMap.CellToLocal(Vector2Int cellPosition) => CellToLocal(cellPosition);
        protected virtual Vector3 CellToLocal(Vector2Int cellPosition) => Vector3.zero;

        int IMap.CellToSortingOrder(Vector2Int cellPosition) => CellToSortingOrder(cellPosition);
        protected virtual int CellToSortingOrder(Vector2Int cellPosition) => 0;

        void IMap.AddElement(IMapElement element) => AddElement(element);
        protected virtual void AddElement(IMapElement element) { }

        void IMap.RemoveElement(IMapElement element) => RemoveElement(element);
        protected virtual void RemoveElement(IMapElement element) { }

        bool IMap.InBounds(Vector2Int location) => InBounds(location);
        protected virtual bool InBounds(Vector2Int location) => false;

        bool IMap.GetIsElementOnMap(IMapElement element) => GetIsElementOnMap(element);
        protected virtual bool GetIsElementOnMap(IMapElement element) => false;

        List<T> IMap.GetAllMapElements<T>() => GetAllMapElements<T>();
        protected virtual List<T> GetAllMapElements<T>() => new List<T>();

        T IMap.GetMapElementAtCell<T>(Vector2Int cell) => GetMapElementAtCell<T>(cell);
        protected virtual T GetMapElementAtCell<T>(Vector2Int cell) => default;

        List<T> IMap.GetMapElementsAtCell<T>(Vector2Int cell) => GetMapElementsAtCell<T>(cell);
        protected virtual List<T> GetMapElementsAtCell<T>(Vector2Int cell) => new List<T>();

        List<T> IMap.GetMapElementsAtCells<T>(List<Vector2Int> cells) => GetMapElementsAtCells<T>(cells);
        protected virtual List<T> GetMapElementsAtCells<T>(List<Vector2Int> cells) => new List<T>();

        List<T> IMap.GetMapElementsInBounds<T>(int x, int y, int width, int height) => GetMapElementsInBounds<T>(x, y, width, height);
        protected virtual List<T> GetMapElementsInBounds<T>(int x, int y, int width, int height) => new List<T>();

        List<T> IMap.GetMapElementsInRadius<T>(Vector2Int centerCell, int radius) => GetMapElementsInRadius<T>(centerCell, radius);
        protected virtual List<T> GetMapElementsInRadius<T>(Vector2Int centerCell, int radius) => new List<T>();

        string IDescribable.DisplayName => DisplayName;
        protected virtual string DisplayName { get; }

        string IDescribable.Description => Description;
        protected virtual string Description { get; }
    }
}
