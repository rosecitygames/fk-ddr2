using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Maps;
using RCG.Attributes;

namespace RCG.Demo.Simulator
{
    [RequireComponent(typeof(Grid))]
    public class Map : AbstractMap
    {
        [SerializeField]
        string displayName = "";
        public override string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        [SerializeField]
        [TextArea]
        string description = "";
        public override string Description
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

        public override Vector3Int LocalToCell(Vector3 localPosition)
        {
            return grid.LocalToCell(localPosition);
        }

        public override Vector3 CellToLocal(Vector3Int cellPosition)
        {
            return grid.CellToLocal(cellPosition);
        }

        private Dictionary<int, List<IMapElement>> hashIdToMapElement = new Dictionary<int, List<IMapElement>>();
        private Dictionary<IMapElement, int> mapElementToHashId = new Dictionary<IMapElement, int>();

        const int primeX = 73856093;
        const int primeY = 19349663;

        int GetHashId(IMapElement mapElement)
        {
            Vector3Int location = mapElement.Location;
            return (location.x * primeX) ^ (location.y * primeY);
        }

        public override void AddElement(IMapElement mapElement)
        {
            if (mapElementToHashId.ContainsKey(mapElement))
            {
                hashIdToMapElement[mapElementToHashId[mapElement]].Remove(mapElement);
            }

            int key = GetHashId(mapElement);
            if (hashIdToMapElement.ContainsKey(key))
            {
                hashIdToMapElement[key].Add(mapElement);
            }
            else
            {
                hashIdToMapElement[key] = new List<IMapElement> { mapElement };
            }

            mapElementToHashId[mapElement] = key;
        }
        
        public override void RemoveElement(IMapElement mapElement)
        {
            if (mapElementToHashId.ContainsKey(mapElement))
            {
                hashIdToMapElement[mapElementToHashId[mapElement]].Remove(mapElement);
            }

            mapElementToHashId.Remove(mapElement);
        }

        
    }
}

