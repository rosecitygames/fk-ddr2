using RCG.Attributes;
using System.Collections.Generic;
using UnityEngine;
using RCG.Paths;

namespace RCG.Maps
{
    public class NullMap : IMap
    {
        Vector2Int IMap.Size => Vector2Int.zero;

        Vector3 IMap.CellSize => Vector3.zero;

        int IMap.CellCount => 0;

        Vector2Int IMap.LocalToCell(Vector3 localPosition) => Vector2Int.zero;
        Vector3 IMap.CellToLocal(Vector2Int cellPosition) => Vector3.zero;
        int IMap.CellToSortingOrder(Vector2Int cellPosition) => 0;

        void IMap.AddElement(IMapElement element) { }
        void IMap.RemoveElement(IMapElement element) { }

        bool IMap.InBounds(Vector2Int location) => false;
        bool IMap.GetIsElementOnMap(IMapElement element) => false;

        List<T> IMap.GetAllMapElements<T>() => new List<T>();
        T IMap.GetMapElementAtCell<T>(Vector2Int cell) => default;
        List<T> IMap.GetMapElementsAtCell<T>(Vector2Int cell) => new List<T>();
        List<T> IMap.GetMapElementsAtCells<T>(List<Vector2Int> cells) => new List<T>();
        List<T> IMap.GetMapElementsInBounds<T>(int x, int y, int width, int height) => new List<T>();
        List<T> IMap.GetMapElementsInRadius<T>(Vector2Int centerCell, int radius) => new List<T>();

        string IDescribable.DisplayName => "";
        string IDescribable.Description => "";
        
        public static IMap Create()
        {
            return new NullMap();
        }
    }
}

