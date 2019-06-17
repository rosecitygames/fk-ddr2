using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Utils;

namespace RCG.Maps
{
    [RequireComponent(typeof(Grid))]
    public class GridMap : AbstractMap
    {
        [SerializeField]
        string displayName = "";
        protected override string DisplayName => displayName;

        [SerializeField]
        [TextArea]
        string description = "";
        protected override string Description => description;

        Grid grid;
        Grid Grid
        {
            get
            {
                if (grid == null)
                {
                    grid = GetComponent<Grid>();
                }
                return grid;
            }
        }

        [SerializeField]
        Vector2Int size = new Vector2Int(10, 10);
        protected override Vector2Int Size => size;

        protected override Vector3 CellSize => Grid.cellSize;

        protected override Vector2Int LocalToCell(Vector3 localPosition)
        {
            Vector3Int gridCell = Grid.LocalToCell(localPosition);
            return new Vector2Int(gridCell.x, gridCell.y);
        }

        protected override Vector3 CellToLocal(Vector2Int cell)
        {
            Vector3Int gridCell = new Vector3Int(cell.x, cell.y, 0);
            Vector3 localPosition = Grid.CellToLocal(gridCell);
            localPosition.x += Grid.cellSize.x * 0.5f;
            localPosition.y += Grid.cellSize.y * 0.5f;
            return localPosition;
        }

        protected override int CellToSortingOrder(Vector2Int cellPosition)
        {
            return cellPosition.y * -100;
        }

        private Dictionary<int, List<IMapElement>> hashIdToMapElement = new Dictionary<int, List<IMapElement>>();
        private Dictionary<IMapElement, int> mapElementToHashId = new Dictionary<IMapElement, int>();

        const int primeX = 73856093;
        const int primeY = 19349663;

        int GetHashId(IMapElement mapElement)
        {
            Vector2Int location = mapElement.Location;
            return GetHashId(location);
        }

        int GetHashId(Vector2Int cell)
        {
            cell.x += size.x / 2;
            cell.y += size.y / 2;
            return (cell.x * primeX) ^ (cell.y * primeY);
        }

        protected override void AddElement(IMapElement mapElement)
        {
            if (mapElementToHashId.ContainsKey(mapElement))
            {
                hashIdToMapElement[mapElementToHashId[mapElement]].Remove(mapElement);
            }

            int cellHashId = GetHashId(mapElement);
            if (hashIdToMapElement.ContainsKey(cellHashId))
            {
                hashIdToMapElement[cellHashId].Add(mapElement);
            }
            else
            {
                hashIdToMapElement[cellHashId] = new List<IMapElement> { mapElement };
            }

            mapElementToHashId[mapElement] = cellHashId;
        }

        protected override void RemoveElement(IMapElement mapElement)
        {
            if (mapElementToHashId.ContainsKey(mapElement))
            {
                hashIdToMapElement[mapElementToHashId[mapElement]].Remove(mapElement);
            }

            mapElementToHashId.Remove(mapElement);
        }

        protected override bool InBounds(Vector2Int location)
        {
            int maxX = Size.x / 2;
            int minX = -maxX;
            int maxY = Size.y / 2;
            int minY = -maxY;
            return location.x >= minX && location.x <= maxX && location.y >= minY && location.y <= maxY; 
        }

        protected override bool GetIsElementOnMap(IMapElement mapElement)
        {
            return mapElementToHashId.ContainsKey(mapElement);
        }

        protected override List<T> GetAllMapElements<T>()
        {
            List<IMapElement> mapElements = new List<IMapElement>(mapElementToHashId.Keys);
            return ConvertMapElementsList<T>(mapElements);
        }

        protected override T GetMapElementAtCell<T>(Vector2Int cell)
        {
            List<IMapElement> mapElements = null;

            int cellHashId = GetHashId(cell);
            if (hashIdToMapElement.ContainsKey(cellHashId))
            {
                mapElements = hashIdToMapElement[cellHashId];
            }
            else
            {
                mapElements = new List<IMapElement>();
            }

            foreach(T typeElement in mapElements)
            {
                if (typeElement != null) return typeElement;
            }
            return default;
        }

        protected override List<T> GetMapElementsAtCell<T>(Vector2Int cell)
        {
            int cellHashId = GetHashId(cell);
            if (hashIdToMapElement.ContainsKey(cellHashId))
            {
                List<IMapElement> mapElements = hashIdToMapElement[cellHashId];
                return ConvertMapElementsList<T>(mapElements);
            }
            else
            {
                return new List<T>();
            }
        }

        protected override List<T> GetMapElementsAtCells<T>(List<Vector2Int> cells)
        {
            List<IMapElement> mapElements = new List<IMapElement>();

            foreach (Vector2Int cell in cells)
            {
                int cellHashId = GetHashId(cell);
                if (hashIdToMapElement.ContainsKey(cellHashId))
                {
                    mapElements.AddRange(hashIdToMapElement[cellHashId]);
                }
            }

            return ConvertMapElementsList<T>(mapElements);
        }

        protected override List<T> GetMapElementsInBounds<T>(int x, int y, int width, int height)
        {
            if (x < 0)
            {
                width -= x;
                x = 0;
            }

            int maxX = x + width;
            if (maxX >= Size.x)
            {
                width = Size.x - x;
            }

            if (y < 0)
            {
                height -= y;
                y = 0;
            }

            int maxY = y + height;
            if (maxY >= Size.y)
            {
                height = Size.y - y;
            }

            List<Vector2Int> cells = new List<Vector2Int>();
            for (int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    Vector2Int cell = new Vector2Int(x + i, y + j);
                    cells.Add(cell);
                }
            }

            return GetMapElementsAtCells<T>(cells);
        }

        protected override List<T> GetMapElementsInRadius<T>(Vector2Int centerCell, int radius)
        {
            HashSet<Vector2Int> cells = GridUtil.GetCellsInsideRadius(Size, centerCell, radius, true);
            return GetMapElementsAtCells<T>(new List<Vector2Int>(cells));
        }

        List<T> ConvertMapElementsList<T>(List<IMapElement> mapElements)
        {
            List<T> typeElements = new List<T>();
            foreach (T typeElement in mapElements)
            {
                typeElements.Add(typeElement);
            }
            return typeElements;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 bounds = GetComponent<Grid>().cellSize;
            bounds.x *= Size.x * transform.lossyScale.x;
            bounds.y *= Size.y * transform.lossyScale.y;
            Gizmos.DrawWireCube(transform.position, bounds);
        }
    }
}

