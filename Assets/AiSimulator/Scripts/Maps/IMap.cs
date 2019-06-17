using RCG.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Maps
{
    public interface IMap : IDescribable
    {
        Vector2Int Size { get; }
        Vector3 CellSize { get; }
        int CellCount { get; }

        Vector2Int LocalToCell(Vector3 localPosition);
        Vector3 CellToLocal(Vector2Int cellPosition);
        int CellToSortingOrder(Vector2Int cellPosition);

        void AddElement(IMapElement element);
        void RemoveElement(IMapElement element);

        bool InBounds(Vector2Int location);
        bool GetIsElementOnMap(IMapElement element);

        List<T> GetAllMapElements<T>();
        T GetMapElementAtCell<T>(Vector2Int cell);
        List<T> GetMapElementsAtCell<T>(Vector2Int cell);
        List<T> GetMapElementsAtCells<T>(List<Vector2Int> cells);
        List<T> GetMapElementsInBounds<T>(int x, int y, int width, int height);
        List<T> GetMapElementsInRadius<T>(Vector2Int centerCell, int radius);
    }
}
