using System;
using System.Collections.Generic;
using UnityEngine;
using RCG.Maps;

namespace RCG.Paths
{
    public class WeightedGraph : IWeightedGraph<Vector2Int>
    {
        static readonly Vector2Int[] adjacentDirections = new[]
        {
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.up
        };

        IMap map = null;
        string impassableAttribute = "";
        Vector2Int? impassableException = null;

        int IWeightedGraph<Vector2Int>.Cost(Vector2Int a, Vector2Int b) => Cost(a, b);
        int Cost(Vector2Int a, Vector2Int b) => 0;

        IEnumerable<Vector2Int> IWeightedGraph<Vector2Int>.Neighbors(Vector2Int id) => Neighbors(id);
        IEnumerable<Vector2Int> Neighbors(Vector2Int id)
        {
            foreach (Vector2Int adjacentDirection in adjacentDirections)
            {
                Vector2Int next = new Vector2Int(id.x + adjacentDirection.x, id.y + adjacentDirection.y);

                bool isInBounds = map.InBounds(next);
                bool isPassable = (Passable(next) || (impassableException != null && next == impassableException));
                if (isInBounds && isPassable)
                {
                    yield return next;
                }
            }
        }

        bool Passable(Vector2Int location)
        {
            List<IMapElement> mapElements = map.GetMapElementsAtCell<IMapElement>(location);
            foreach (IMapElement mapElement in mapElements)
            {
                if (string.IsNullOrEmpty(impassableAttribute) == false && mapElement.GetStat(impassableAttribute).Quantity > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static IWeightedGraph<Vector2Int> Create(IMap map, string impassableAttribute = "", Vector2Int? impassableException = null)
        {
            return new WeightedGraph
            {
                map = map,
                impassableAttribute = impassableAttribute,
                impassableException = impassableException
            };
        }
    }
}
