using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Maps;
using RCG.Attributes;

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
        Vector3Int size = new Vector3Int(10, 10, 0);
        protected override Vector3Int Size => size;

        protected override Vector3 CellSize => Grid.cellSize;

        protected override Vector3Int LocalToCell(Vector3 localPosition)
        {
            return Grid.LocalToCell(localPosition);
        }

        protected override Vector3 CellToLocal(Vector3Int cell)
        {
            Vector3 localPosition = Grid.CellToLocal(cell);
            localPosition.x += Grid.cellSize.x * 0.5f;
            localPosition.y += Grid.cellSize.y * 0.5f;
            return localPosition;
        }

        protected override int CellToSortingOrder(Vector3Int cellPosition)
        {
            return cellPosition.y * -100;
        }

        private Dictionary<int, List<IMapElement>> hashIdToMapElement = new Dictionary<int, List<IMapElement>>();
        private Dictionary<IMapElement, int> mapElementToHashId = new Dictionary<IMapElement, int>();

        const int primeX = 73856093;
        const int primeY = 19349663;

        int GetHashId(IMapElement mapElement)
        {
            Vector3Int location = mapElement.Location;
            return GetHashId(location);
        }

        int GetHashId(Vector3Int cell)
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

        protected override List<IMapElement> GetMapElementsAtCells(List<Vector3Int> cells)
        {
            List<IMapElement> mapElements = new List<IMapElement>();

            foreach (Vector3Int cell in cells)
            {
                int cellHashId = GetHashId(cell);
                if (hashIdToMapElement.ContainsKey(cellHashId))
                {
                    mapElements.AddRange(hashIdToMapElement[cellHashId]);
                }
            }

            return mapElements;
        }

        protected override List<IMapElement> GetMapElementsAtCell(Vector3Int cell)
        {
            int cellHashId = GetHashId(cell);
            if (hashIdToMapElement.ContainsKey(cellHashId))
            {
                return hashIdToMapElement[cellHashId];
            }
            else
            {
                return new List<IMapElement>();
            }
        }

        List<IMapElement> GetMapElementsInRect(Vector3Int center, int width, int height)
        {
            List<IMapElement> mapElements = new List<IMapElement>();

            int offsetX = -Mathf.FloorToInt(width / 2);
            int offsetY = -Mathf.FloorToInt(height / 2);

            Vector3Int cell = new Vector3Int();

            for (int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    cell.x = center.x + x + offsetX;
                    cell.y = center.y + y + offsetY;
                    List<IMapElement> cellMapElements = GetMapElementsAtCell(cell);
                    mapElements.AddRange(cellMapElements);
                }
            }

            return mapElements;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 bounds = GetComponent<Grid>().cellSize;
            bounds.x *= Size.x * transform.lossyScale.x;
            bounds.y *= Size.y * transform.lossyScale.y;
            bounds.z *= Size.z * transform.lossyScale.z;
            Gizmos.DrawWireCube(transform.position, bounds);
        }
    }
}

