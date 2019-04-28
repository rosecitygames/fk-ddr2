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
        protected override string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        [SerializeField]
        [TextArea]
        string description = "";
        protected override string Description
        {
            get
            {
                return description;
            }
        }

        Grid grid;
        private void Awake()
        {
            grid = GetComponent<Grid>();
        }


        [SerializeField]
        Vector3Int size = new Vector3Int(10, 10, 0);
        protected override Vector3Int Size
        {
            get
            {
                return size;
            }
        }

        protected override Vector3 CellSize
        {
            get
            {
                return grid.cellSize;
            }
        }

        protected override Vector3Int LocalToCell(Vector3 localPosition)
        {
            if (grid != null)
            {
                return grid.LocalToCell(localPosition);
            }
            else
            {
                return Vector3Int.zero;
            }
        }

        protected override Vector3 CellToLocal(Vector3Int cellPosition)
        {
            if (grid != null)
            {
                Vector3 localPosition = grid.CellToLocal(cellPosition);
                localPosition.x += grid.cellSize.x * 0.5f;
                localPosition.y += grid.cellSize.y * 0.5f;
                return localPosition;
            }
            else
            {
                return Vector3Int.zero;
            }
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 bounds = GetComponent<Grid>().cellSize;
            bounds.x *= Size.x;
            bounds.y *= Size.y;
            bounds.z *= Size.z;
            Gizmos.DrawWireCube(transform.position, bounds);
        }
    }
}

